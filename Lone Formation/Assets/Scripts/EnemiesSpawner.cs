using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
	EnemiesManager manager;
	[Tooltip("This use to scale with difficulty\nThe lower it is the more enemy will spawn")]
	public float delay;
	[SerializeField] float spawnPerSecond; float spawnTimer;
	[System.Serializable] class SpawnData {public GameObject enemy; public float rate;}
	[SerializeField] SpawnData[] table;

    void Start()
	{
		manager = EnemiesManager.i;
	}

	void Update()
	{
		//Get enemy spawning frequent that scale with difficulty
		spawnPerSecond = delay / manager.difficulty;
		//An basic timer for spawning enemy
		spawnTimer += Time.deltaTime;
		if(spawnTimer >= spawnPerSecond)
		{
			ChooseWhichEnemyToSpawn();
			spawnTimer -= spawnTimer;
		}
	}

	void ChooseWhichEnemyToSpawn()
	{
		//Get the total rate of all data to get the chance gonna use
		float total = 0; for (int d = 0; d < table.Length; d++) total += table[d].rate;
		float chance = Random.Range(0, total);
		//Choose which enemy will spawn using weight system
		for (int d = 0; d < table.Length; d++)
		{
			if((chance - table[d].rate) <= 0)
			{
				SpawnEnemyAtItWantedPosition(table[d].enemy.GetComponent<Enemy>());
			}
			else
			{
				chance -= table[d].rate;
			}
		}
		
	}

	void SpawnEnemyAtItWantedPosition(Enemy enemy)
	{
		//Get the map scale
		Vector2 scale = Map.i.scale;
		//@ Spawn in enemy in xy axis basr on it direction
		switch((int)enemy.spawning.direction)
		{
			case 0:
				manager.SpawnEnemy(enemy, new Vector2(SpawnCenterPercent(enemy, scale.x), scale.y));
			break;
			case 1:
				manager.SpawnEnemy(enemy, new Vector2(SpawnCenterPercent(enemy, scale.x), -scale.y));
			break;
			case 2:
				manager.SpawnEnemy(enemy, new Vector2(-scale.x, SpawnCenterPercent(enemy, scale.y)));
			break;
			case 3:
				manager.SpawnEnemy(enemy, new Vector2(scale.x, SpawnCenterPercent(enemy, scale.y)));
			break;
		}
	}

	float SpawnCenterPercent(Enemy enemy, float axis)
	{
		//How many percent able to spawn outside the center
		float centering = (enemy.spawning.focusCenter/100) * axis;
		return Random.Range(-axis + centering, axis - centering);
	}
}
