using UnityEngine;

public class Caster : MonoBehaviour
{
	public Stats stats;
	public GameObject attack;
	float timer;

	void Update()
	{
		//Set pattern when timer reach cooldown
		timer += Time.deltaTime;
		if(timer >= stats.cooldown)
		{
			SetPattern();
			timer -= timer;
		}
	}

	public virtual void SetPattern() {}

	protected void CreateAttack(Vector2 pos, Quaternion rot)
	{
		//Create the attack then set it damage and velocity from stats
		GameObject attacked = Pool.i.Create(attack, pos, rot);
	}
}
