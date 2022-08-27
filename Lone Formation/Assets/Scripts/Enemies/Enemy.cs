using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Stats stats;
	public EnemyMovement movement;
	public int bounty;
	public Spawning spawning; [System.Serializable] public class Spawning
	{	
		public Direction direction;
		public enum Direction {up,down,left,right}
		[Tooltip("How much % will it focus spawning in the center")] [Range(0,100)]
		public float focusCenter;
	}

	public virtual void Start()
	{
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
