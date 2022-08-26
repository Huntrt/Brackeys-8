using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Leader : MonoBehaviour
{
    //Set this class to singleton
	public static Leader i {get{if(_i==null){_i = GameObject.FindObjectOfType<Leader>();}return _i;}} static Leader _i;

    public List<Allies> allies = new List<Allies>();
	public Stats stats;
	public LeaderMovement movement;
	public Formation formation;
	[Header("Heath")]
	[SerializeField] Image heathBar;
	[SerializeField] TMPro.TextMeshProUGUI heathCounter;

	void Update()
	{
		DisplayHeath();
	}

	void DisplayHeath()
	{
		//Display the counter and bar
		heathCounter.text = stats.health + "/" + stats.maxHealth;
		heathBar.fillAmount = (float)stats.health / (float)stats.maxHealth;
	}

	public void AddAllies(Allies add)
	{
		allies.Add(add);
		formation.CreateGoal();
	}

	public void RemoveAllies(Allies remove)
	{
		allies.Remove(remove);
		formation.CreateGoal();
	}

}