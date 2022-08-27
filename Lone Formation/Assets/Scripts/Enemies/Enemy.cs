using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Stats stats;
	public EnemyMovement movement;
	public int bounty;
	public float danger;
	[Tooltip("How many percent of difficulty with increase with max hp")]
	public float healthScaling;
	public Spawning spawning; [System.Serializable] public class Spawning
	{	
		public Direction direction;
		public enum Direction {up,down,left,right}
		[Tooltip("How much % will it focus spawning in the center")] [Range(0,100)]
		public float focusCenter;
	}

	public virtual void Start()
	{
		//Scaling max heath (Heath got increade by X% of difficulty [Hp25 + (20% of dif12) = 27.4])
		stats.maxHealth += (int)(EnemiesManager.i.difficulty * (healthScaling / 100f));
		stats.health = stats.maxHealth;
		stats.healthFunction.takeDamage += TakingDamage;
		//Are killed when die via out of health
		stats.healthFunction.onDie += Killed;
	}

	void TakingDamage()
	{
		Audios.i.enemyHurtPlay();
	}

	void Killed()
	{
		Audios.i.enemyDiePlay();
		//Earn bounty then kill it in manager
		Economic.i.Earn(bounty);
		EnemiesManager.i.KillEnemy(this);
	}

	void OnDisable()
	{
		stats.healthFunction.takeDamage -= TakingDamage;
		stats.healthFunction.onDie -= Killed;
	}
}
