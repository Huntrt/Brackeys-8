using UnityEngine;

public class CasterAccuracy : Caster
{
    public float spread;
	public int amount;

	public override void SetPattern()
	{
		base.SetPattern();
		//Randomly choose an rotation for each attack
		for (int a = 0; a < amount; a++)
		{
			CreateAttack(transform.position, Quaternion.Euler(0,0,Random.Range(-spread,spread)));
		}
	}
}
