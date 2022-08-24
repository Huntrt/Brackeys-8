using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Stats stats;
	public EnemyMovement movement;	
	public Spawning spawning; [System.Serializable] public class Spawning
	{	
		public Direction direction;
		public enum Direction {up,down,left,right}
		[Tooltip("How much % will it focus spawning in the center")] [Range(0,100)]
		public float focusCenter;
	}
}
