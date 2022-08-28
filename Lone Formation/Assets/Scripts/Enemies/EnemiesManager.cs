using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    //Set this class to singleton
	public static EnemiesManager i {get{if(_i==null){_i = GameObject.FindObjectOfType<EnemiesManager>();}return _i;}} static EnemiesManager _i;

	public float difficulty;
	public List<Enemy> enemies = new List<Enemy>();
	public EnemiesSpawner spawner;
	public int enemyKilled;

	public void SpawnEnemy(Enemy enemy, Vector2 position)
	{
		GameObject spawned = Instantiate(enemy.gameObject, position ,enemy.transform.localRotation);
		enemies.Add(spawned.GetComponent<Enemy>());
	}

	public void KillEnemy(Enemy enemy)
	{
		enemyKilled++;
		difficulty += enemy.danger;
		//note: Decrease the weight of an null slot of enemy make more enemy spawn and also
		//it also help increase more weight allowed when go negative for more powerful enemy spawn
		spawner.enemyTable[0].weight -= enemy.danger*15;
		//Also scale wreck spawn rate
		AlliesSpawner.i.wreckTable.weightList[0].weight -= enemy.danger*3;
		DespawnEnemy(enemy);
	}

	public void DespawnEnemy(Enemy enemy)
	{
		enemies.Remove(enemy);
		Destroy(enemy.gameObject);
	}
}
