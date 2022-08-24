using System.Collections.Generic;
using UnityEngine;

public class Allies : MonoBehaviour
{
	[SerializeField] Stats stats;
	[SerializeField] Rigidbody2D rb;
	Formation formation;
	public int id;

	void Start()
	{
		formation = Leader.i.formation;
		Leader.i.AddAllies(this);
	}

    void FixedUpdate()
	{
		//Get the goal position at it id in formation 
		Vector2 goal = formation.goals[id-1].position;
		//Get direction toward the goal 
		Vector2 dir = goal - (Vector2)rb.position;
		//Moving toward that direction of that goal
		rb.MovePosition(rb.position + (dir * stats.movementSpeed) * Time.fixedDeltaTime);
	}
}
