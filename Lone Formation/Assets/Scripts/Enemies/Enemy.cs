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

	void Start()
	{
		//Are killed when die via out of health
		stats.healthFunction.onDie += Killed;
	}

	void Killed()
	{
		stats.healthFunction.onDie -= Killed;
		//Earn bounty then kill it in manager
		Economic.i.Earn(bounty);
		EnemiesManager.i.KillEnemy(this);
	}
}
