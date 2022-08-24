using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    //Set this class to singleton
	public static Leader i {get{if(_i==null){_i = GameObject.FindObjectOfType<Leader>();}return _i;}} static Leader _i;

    public List<Allies> allies = new List<Allies>();
	public Formation formation;
	public LeaderMovement movement;
	public Stats stats;

	public void AddAllies(Allies add)
	{
		allies.Add(add);
		//todo: Make sure to fix id when remove allies
		add.id = allies.Count;
		formation.CreateGoal();
	}
}