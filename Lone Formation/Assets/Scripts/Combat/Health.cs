using UnityEngine;
using System;

public class Health : MonoBehaviour
{
	[SerializeField] Stats stats;
	[SerializeField] bool destroy = true;
	[SerializeField] GameObject onHitParticle, dieParticle;
	public event Action takeDamage, onDie;

    public void TakeDamage(int taken)
	{
		//Take damage and destroy it when it die
		stats.health -= taken;
		Pool.i.Create(onHitParticle, transform.position, Quaternion.identity);
		takeDamage?.Invoke();
		if(stats.health <= 0)
		{
			Pool.i.Create(dieParticle, transform.position, Quaternion.identity);
			onDie?.Invoke();
			//Only disable if leader
			if(destroy) Destroy(gameObject);
		}
	}
}
