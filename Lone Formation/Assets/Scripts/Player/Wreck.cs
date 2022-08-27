using UnityEngine;
using TMPro;

public class Wreck : MonoBehaviour
{
	[SerializeField] GameObject ship;
	[SerializeField] int cost;
	[SerializeField] float speed, hitbox, lifeTime;
	[SerializeField] Rigidbody2D rb;
	[Header("Interface")]
	[SerializeField] Transform display;
	[SerializeField] SpriteRenderer highlight;
	[SerializeField] TextMeshProUGUI costDisplay;
	[SerializeField] Color poor, rich;
	[SerializeField] GameObject buyParticle;
	int alliesLayer;

    void Start()
    {
		//Get the allies layer
		alliesLayer = LayerMask.GetMask("Allies");
		//Randomly rotation upon spawn
        display.localRotation = Quaternion.Euler(0,0,Random.Range(0f,360f));
		highlight.transform.localRotation = display.localRotation;
		//Set display cost and set interface to poor
		costDisplay.text = "$" + cost; highlight.color = poor; costDisplay.color = poor;
		Destroy(gameObject, lifeTime);
    }

	void Update()
	{
		//Auto hide interface
		highlight.gameObject.SetActive(false);
		costDisplay.transform.parent.gameObject.SetActive(false);
		//Create circle hitbox to detect if allies in it
		RaycastHit2D hit = Physics2D.CircleCast(transform.position, hitbox, Vector2.zero, 0, alliesLayer);
		//If any allies in this wreck hitbox
		if(hit) 
		{
			//Show interface then set it to poor
			highlight.gameObject.SetActive(true);
			costDisplay.transform.parent.gameObject.SetActive(true);
			highlight.color = poor; costDisplay.color = poor;
			//If has enough to buy this wreck
			if(Economic.i.Spended(cost, false))
			{
				//Set interface to rich
				highlight.color = rich; costDisplay.color = rich;
				//Buying this wreck when press the key
				if(Input.GetKey(KeyManager.i.Buy))
				{
					Audios.i.buyPlay();
					Pool.i.Create(buyParticle, transform.position, Quaternion.identity);
					Economic.i.Spended(cost, true);
					Instantiate(ship, transform.position, Quaternion.identity);
					Destroy(gameObject);
				}
			}
		}
		
	}

	void FixedUpdate()
	{
		//Move the wreck backward of the red arrow with speed has get
		rb.MovePosition(rb.position + (Vector2.left * speed) * Time.fixedDeltaTime);
	}
}
