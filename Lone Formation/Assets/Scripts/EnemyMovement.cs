using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[HideInInspector] int currentId = -1;
	[HideInInspector] public Action curSequence;
	[HideInInspector] public float timer;
	[HideInInspector] bool outOfMovement;
	public Action[] sequence;
	public Enemy enemy;
	public Rigidbody2D rb;

	public enum Move
	{
		Up, Down, Left, Right,
		FaceOff, Hover, Chasing
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
		//Get the current move and speed to use
		Move move = curSequence.move;
		float speed = enemy.stats.movementSpeed;
		Vector2 direction = Vector2.zero;
		//@ Basic Direction
		if(move == Move.Up)    direction = new Vector2(0,+1);
		if(move == Move.Down)  direction = new Vector2(0,-1);
		if(move == Move.Left)  direction = new Vector2(-1,0);
		if(move == Move.Right) direction = new Vector2(+1,0);
		//todo: advanced movement
		//Moving toward direction with speed has get
		rb.MovePosition(rb.position + (direction * speed) * Time.fixedDeltaTime);
		//Go to the next sequence if timer has reach current duration
		timer += Time.fixedDeltaTime;
		if(timer >= curSequence.duration) NextSequence();
	}

	void NextSequence()
	{
		currentId++;
		if(currentId > sequence.Length-1) {outOfMovement = true; return;}
		curSequence = sequence[currentId];
		timer -= timer;
	}
}