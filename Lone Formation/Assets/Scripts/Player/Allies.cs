using System.Collections.Generic;
using UnityEngine;

public class Allies : MonoBehaviour
{
	[SerializeField] Stats stats;
	[SerializeField] Rigidbody2D rb;
	Formation formation;
	public int id;
	Map map;

	void Start()
	{
		map = Map.i;
		formation = Leader.i.formation;
		Leader.i.AddAllies(this);
	}

    void FixedUpdate()
	{
		//Get the goal position at it id in formation 
		Vector2 goal = formation.goals[id-1].position;
		//Get direction toward the goal 
		Vector2 dir = goal - (Vector2)rb.position;
		//Prevent allies from go out of map
		rb.position = new Vector2
		(
			Mathf.Clamp(rb.position.x, -map.scale.x, map.scale.x),
			Mathf.Clamp(rb.position.y, -map.scale.y, map.scale.y)
		);
		//Moving toward that direction of that goal
		rb.MovePosition(rb.position + (dir * stats.movementSpeed) * Time.fixedDeltaTime);
	}
}
