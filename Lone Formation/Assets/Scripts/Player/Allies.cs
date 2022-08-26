using UnityEngine;

public class Allies : MonoBehaviour
{
	[SerializeField] Stats stats;
	[SerializeField] Rigidbody2D rb;
	Formation formation;
	Transform goal;
	Map map;

	void Start()
	{
		map = Map.i;
		formation = Leader.i.formation;
		Leader.i.AddAllies(this);
		stats.healthFunction.onDie += Killed;
	}

	void Killed()
	{
		Leader.i.RemoveAllies(this);
		Destroy(gameObject);
		stats.healthFunction.onDie -= Killed;
	}

	public void GetGoal()
	{
		//Get the goal of this allies
		for (int a = 0; a < formation.goals.Length; a++)
		{
			if(Leader.i.allies[a] == this) goal = formation.goals[a];
		}
	}

    void FixedUpdate()
	{
		//Get direction toward the goal 
		Vector2 dir = (Vector2)goal.position - rb.position;
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
