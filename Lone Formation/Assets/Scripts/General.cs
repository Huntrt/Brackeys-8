using UnityEngine;
using System;

[Serializable] public class WeightData {public GameObject obj; public float weight;}

public class General 
{
	public static GameObject WeightingResult(WeightData[] data)
	{
		//Get the total weight of all data to get the chance gonna use
		float total = 0; for (int d = 0; d < data.Length; d++) total += data[d].weight;
		//Randomly choose an number from 0 to total
		float chance = UnityEngine.Random.Range(0, total);
		//Choose which object will return using weight method
		for (int o = 0; o < data.Length; o++)
		{
			if((chance - data[o].weight) <= 0)
			{
				return data[o].obj;
			}
			else
			{
				chance -= data[o].weight;
			}
		}
		return null;
	}
}
