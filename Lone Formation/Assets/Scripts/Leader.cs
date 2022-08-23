using UnityEngine;

public class Leader : MonoBehaviour
{
    //Set this class to singleton
	public static Leader i {get{if(_i==null){_i = GameObject.FindObjectOfType<Leader>();}return _i;}} static Leader _i;

	public Formation formation;
	public LeaderMovement movement;
}