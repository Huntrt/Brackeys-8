using UnityEngine;

public class AlliesSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] alliesToUse;
    [SerializeField] Table wreckTable;
	Map map;

	[System.Serializable] class Table
	{
		public float delay, perSecond;
		[HideInInspector] public float timer;
	    public WeightData[] weightList;
	}

	void Start()
	{
		map = Map.i;
	}

	void Update()
	{
		CountingSpawn();
	}

	void CountingSpawn()
	{
		wreckTable.timer += Time.deltaTime;
		if(wreckTable.timer >= wreckTable.delay) {SpawnWreck(); wreckTable.timer -= wreckTable.timer;}
	}

	void SpawnWreck()
	{
		//Get randomly position to spawn in Y axis of map border
		Vector2 pos = new Vector2(map.scale.x, Random.Range(-map.scale.y, map.scale.y));
		//Weight tochoose which wreck will be spawn
		GameObject spawn = General.WeightingResult(wreckTable.weightList);
		if(spawn != null) Instantiate(spawn, pos, Quaternion.identity);
	}
}
