using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
	[SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI survive, killed, allies;
	[SerializeField] GameObject[] hideUIs;
	Leader leader; int peakAllies;

	void Start()
	{
		leader = Leader.i;
		//Game over when leader die
		leader.stats.healthFunction.onDie += Over;
	}

	void Update()
	{
		//Record the most amount of allies get
		if(leader.allies.Count > peakAllies) peakAllies = leader.allies.Count ;
	}

	void Over()
	{
		survive.text = "Survive for <b>" + Time.time + "</b> second";
		killed.text = "Killed <b>" + EnemiesManager.i.enemyKilled + "</b> enemy";
		allies.text = "Reached an fleet with <b>" + peakAllies + "</b> ship";
		leader.gameObject.SetActive(false);
		//consider: Hide all UI when die
		for (int g = 0; g < hideUIs.Length; g++) hideUIs[g].SetActive(false);
		//consider: pause game when die
		panel.SetActive(true);
	}
}