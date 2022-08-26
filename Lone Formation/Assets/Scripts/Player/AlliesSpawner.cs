using UnityEngine;

public class AlliesSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] alliesToUse;
    [SerializeField] Table wreckTable, assistTable;
	[System.Serializable] class Table
	{
		public float delay, perSecond; //consider: might scale with difficulty
	    public WeightData[] assistTable;
	}
}
