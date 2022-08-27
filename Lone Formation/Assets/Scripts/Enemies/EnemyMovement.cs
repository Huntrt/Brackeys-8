using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[HideInInspector] int currentId = -1;
	[HideInInspector] public Action curSequence;
	[HideInInspector] public float timer;
	bool outOfMovement, getMoved;
	Vector2 adapted;
	public Action[] sequence;
	public Enemy enemy;
	public Rigidbody2D rb;

	public enum Move
	{
		Still,
		Up, Down, Left, Right,
		FaceOff, Hover, Chasing,
		AdaptX, AdaptY
	}
	[System.Serializable] public class Action 
	{
		[Tooltip("FaceOff = Align with player in X axis\nHover = Align with the player Y axis\n Chasing = follow player")]
		public Move move;
		public float duration;
	}

	void OnEnable()
	{
		//Beginning the sequence
		NextSequence();
	}

	void FixedUpdate()
	{
		//No longer move if out of it
		if(outOfMovement) return;
		float speed = enemy.stats.movementSpeed;
		//Default are standing still
		Vector2 direction = Vector2.zero;
		//Currently are not moving
		getMoved = false;
		//Change direction base on movement currently use if haven't move
		if(!getMoved) direction = DirectionalMovement();
		if(!getMoved) direction = TrackingMovement();
		if(!getMoved) direction = AdaptiveMovement();
		//Moving toward direction with speed has get
		rb.MovePosition(rb.position + (direction * speed) * Time.fixedDeltaTime);
		//Go to the next sequence if timer has reach current duration
		timer += Time.fixedDeltaTime;
		if(timer >= curSequence.duration) NextSequence();
	}

	Vector2 DirectionalMovement()
	{
		getMoved = true;
		//Get the current move and speed to use
		Move move = curSequence.move;
		if(move == Move.Up)    return new Vector2(0,+1);
		if(move == Move.Down)  return new Vector2(0,-1);
		if(move == Move.Left)  return new Vector2(-1,0);
		if(move == Move.Right) return new Vector2(+1,0);
		getMoved = false; return Vector2.zero;
	}

	Vector2 TrackingMovement()
	{
		getMoved = true;
		//Get the current move and speed to use
		Move move = curSequence.move;
		//Get the leader position
		Vector2 lpos = Leader.i.transform.position;
		if(move == Move.FaceOff) return GetDirection(new Vector2(transform.position.x, lpos.y));
		if(move == Move.Hover) 	 return GetDirection(new Vector2(lpos.x, transform.position.y));
		if(move == Move.Chasing) return GetDirection(lpos);
		getMoved = false; return Vector2.zero;
	}

	Vector2 AdaptiveMovement()
	{
		getMoved = true;
		//Get the current move and speed to use
		Move move = curSequence.move;
		//Making sure only adapt axis once per action
		if(adapted == Vector2.zero)
		{
			if(move == Move.AdaptX)
			{
				if(transform.position.x >= 0) adapted = new Vector2(-1,0); 
				else adapted = new Vector2(1,0);
			}
			if(move == Move.AdaptY)
			{
				if(transform.position.y >= 0) adapted = new Vector2(0,-1); 
				else adapted = new Vector2(0,1);
			}
			
		}
		else return adapted;
		getMoved = false; return Vector2.zero;
	}

	Vector2 GetDirection(Vector2 target) {return target - (Vector2)transform.position;}

	void NextSequence()
	{
		currentId++;
		if(currentId > sequence.Length-1) 
		{
			//Despawn enemy when out of movement
			EnemiesManager.i.DespawnEnemy(enemy);
			outOfMovement = true; return;
		}
		adapted = Vector2.zero;
		curSequence = sequence[currentId];
		timer -= timer;
	}
}