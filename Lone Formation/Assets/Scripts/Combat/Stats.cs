using UnityEngine;

public class Stats : MonoBehaviour
{
    public int maxHealth;
	[SerializeField] int _health;
	public Health healthFunction;
	[SerializeField] [Range(0,50)] float _movementSpeed;
	public float cooldown;

	//note: Cap movement speed at 50
	public float movementSpeed 
	{
		get => _movementSpeed; 
		set 
		{
			if(value > 50) value = 50;
			_movementSpeed = value;
		}
	}

	//Prevent health from go out of max health
	public int health 
	{
		get => _health; 
		set 
		{
			if(value > maxHealth) value = maxHealth;
			_health = value;
		}
	}

	void OnEnable()
	{
		_health = maxHealth;
	}
}
