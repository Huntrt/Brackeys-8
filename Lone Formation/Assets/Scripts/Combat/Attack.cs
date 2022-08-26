using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

	protected void AttackOver() {gameObject.SetActive(false);}

	protected void Hitted(GameObject hit)
	{
		
	}
}