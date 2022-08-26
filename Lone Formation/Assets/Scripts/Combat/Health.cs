using UnityEngine;
using System;

public class Health : MonoBehaviour
{
	[SerializeField] Stats stats;
	[SerializeField] bool destroy = true;
	public event Action takenDamage, onDie;

    public void TakeDamage(int taken)
	{
		//Take damage and destroy it when it die
		stats.health -= taken;
		takenDamage?.Invoke();
		if(stats.health <= 0)
		{
			onDie?.Invoke();
			//Only disable if leader
			if(destroy) Destroy(gameObject);
		}
	}
}
