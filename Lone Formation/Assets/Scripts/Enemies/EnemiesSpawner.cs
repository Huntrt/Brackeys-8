using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
	EnemiesManager manager;
	[Tooltip("This use to scale with difficulty\nThe lower it is the more enemy will spawn")]
	public float delay;
	[SerializeField] float spawnPerSecond; float spawnTimer;
	[SerializeField] WeightData[] enemyTable;

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
			//Spawn enemy in an postion after weighted them
			SpawnEnemyInPosition(General.WeightingResult(enemyTable).GetComponent<Enemy>());
			spawnTimer -= spawnTimer;
		}
	}

	void SpawnEnemyInPosition(Enemy enemy)
	{
		//Get the map scale
		Vector2 scale = Map.i.scale;
		//@ Spawn in enemy in xy axis basr on it direction
		switch((int)enemy.spawning.direction)
		{
			case 0:
				manager.SpawnEnemy(enemy, new Vector2(CenteringAxis(enemy, scale.x), scale.y));
			break;
			case 1:
				manager.SpawnEnemy(enemy, new Vector2(CenteringAxis(enemy, scale.x), -scale.y));
			break;
			case 2:
				manager.SpawnEnemy(enemy, new Vector2(-scale.x, CenteringAxis(enemy, scale.y)));
			break;
			case 3:
				manager.SpawnEnemy(enemy, new Vector2(scale.x, CenteringAxis(enemy, scale.y)));
			break;
		}
	}

	float CenteringAxis(Enemy enemy, float axis)
	{
		//How many percent able to spawn outside the center
		float centering = (enemy.spawning.focusCenter/100) * axis;
		return Random.Range(-axis + centering, axis - centering);
	}
}
