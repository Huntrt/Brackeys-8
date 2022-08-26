using UnityEngine;

public class Wreck : MonoBehaviour
{
	[SerializeField] GameObject ship;
	[SerializeField] int cost;
	[SerializeField] float speed, hitbox;
	[SerializeField] Rigidbody2D rb;
	int alliesLayer;

    void Start()
    {
		//Get the allies layer
		alliesLayer = LayerMask.GetMask("Allies");
		//Randomly rotation upon spawn
        transform.localRotation = Quaternion.Euler(0,0,Random.Range(0f,360f));
    }

	void Update()
	{
		//Create circle hitbox to detect if allies in it
		RaycastHit2D hit = Physics2D.CircleCast(transform.position, hitbox, Vector2.zero, 0, alliesLayer);
		//If any allies in this wreck hitbox
		if(hit) 
		{
			//If press buy key while has enough to buy this wreck
			if(Input.GetKey(KeyManager.i.Buy) && Economic.i.Spended(cost))
			{
				Instantiate(ship, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
		
	}

	void FixedUpdate()
	{
		//Move the wreck backward of the red arrow with speed has get
		rb.MovePosition(rb.position + (Vector2.left * speed) * Time.fixedDeltaTime);
	}
}
