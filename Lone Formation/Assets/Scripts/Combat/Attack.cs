using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

	protected void AttackOver()
	{
		//todo: use pooling
		Destroy(gameObject);
	}

	protected void Hitted(GameObject hit)
	{
		
	}
}