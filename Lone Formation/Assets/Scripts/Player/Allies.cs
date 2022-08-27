using UnityEngine.UI;
using UnityEngine;

public class Allies : MonoBehaviour
{
	[SerializeField] Stats stats;
	[SerializeField] Rigidbody2D rb;
	[SerializeField] Image healthBar; 
	[SerializeField] float healthBarFade; float hBTimer;
	Formation formation;
	Transform goal;
	Map map;

	void Start()
	{
		map = Map.i;
		formation = Leader.i.formation;
		Leader.i.AddAllies(this);
		stats.healthFunction.takeDamage += TakingDamage;
		stats.healthFunction.onDie += Killed;
	}

	void Update()
	{	
		DisplayHealthInfo();
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

	void TakingDamage() 
	{
		Audios.i.alliesHurtPlay();
		healthBar.transform.parent.gameObject.SetActive(true); 
		hBTimer = healthBarFade;
	}

	void Killed()
	{
		Audios.i.alliesDiePlay();
		Leader.i.RemoveAllies(this);
		Destroy(gameObject);
	}

	public void GetGoal()
	{
		//Get the goal of this allies
		for (int a = 0; a < formation.goals.Length; a++)
		{
			if(Leader.i.allies[a] == this) goal = formation.goals[a];
		}
	}

	void DisplayHealthInfo()
	{
		//Filling health bar
		healthBar.fillAmount = (float)stats.health / (float)stats.maxHealth;
		//Begin fading after showing health bar
		if(hBTimer > 0) hBTimer -= Time.deltaTime; else healthBar.transform.parent.gameObject.SetActive(false);
	}

	void OnDisable()
	{
		stats.healthFunction.takeDamage -= TakingDamage;
		stats.healthFunction.onDie -= Killed;
	}
}
