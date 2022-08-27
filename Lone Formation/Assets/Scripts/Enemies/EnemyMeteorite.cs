using UnityEngine;

public class EnemyMeteorite : Enemy
{
	public int minHealth, damage;

    public override void Start()
    {
		base.Start();
		//Random rotation
		transform.localRotation = Quaternion.Euler(0,0, Random.Range(0,360));
		//Get an random amount of heath from min to max to use it 
		stats.maxHealth = Random.Range(minHealth, stats.maxHealth+1); stats.health = stats.maxHealth;
		//Set the size according to health has use
		transform.localScale = new Vector2(stats.health/80,stats.health/80);
		//Bounty equal to it heath
		bounty = stats.health;
    }

	private void OnCollisionEnter2D(Collision2D other) 
	{
		//Dealt damage on collision then destroy it self when hit with an allies
		if(!other.gameObject.CompareTag("Allies")) {return;}
		other.gameObject.GetComponent<Health>().TakeDamage(damage);
		stats.healthFunction.TakeDamage(10000000);
	}
}