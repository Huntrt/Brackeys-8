using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    //Set this class to singleton
	public static EnemiesManager i {get{if(_i==null){_i = GameObject.FindObjectOfType<EnemiesManager>();}return _i;}} static EnemiesManager _i;

	public int difficulty;
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
		//consider: difficulty scaling?
		enemyKilled++;
		DespawnEnemy(enemy);
	}

	public void DespawnEnemy(Enemy enemy)
	{
		enemies.Remove(enemy);
		Destroy(enemy.gameObject);
	}
}
