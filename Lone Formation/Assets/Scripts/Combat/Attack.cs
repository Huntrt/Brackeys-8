using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
	public GameObject attackParticle;

	protected void AttackOver() {gameObject.SetActive(false);}

	protected void Attacked(GameObject hit, Vector2 contact)
	{
		//Create attack effect position
		Pool.i.Create(attackParticle, contact, Quaternion.identity);
	}
}