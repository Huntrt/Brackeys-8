using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
	[SerializeField] float speed;
	public Vector3 inputDirection;
	public Rigidbody2D Rigidbody;
	[SerializeField] Camera cam;
	Vector2 mapScale, camScale;

	void Start()
	{
		mapScale = Map.i.scale;
		camScale.y = 2 * cam.orthographicSize;
		camScale.x = camScale.y * cam.aspect;
	}
	
    void Update()
    {
		//Running function
		MoveInput();
	}

	Vector2 velocity; void MoveInput()
	{
		//Set the input horizontal and vertical direction
		inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
		//Prevent movement got slow when hug border
		Vector2 pos = transform.position;
		if(inputDirection.x == 01 && pos.x >= +mapScale.x) inputDirection.x = 0;
		if(inputDirection.x == -1 && pos.x <= -mapScale.x) inputDirection.x = 0;
		if(inputDirection.y == 01 && pos.y >= +mapScale.y) inputDirection.y = 0;
		if(inputDirection.y == -1 && pos.y <= -mapScale.y) inputDirection.y = 0;
        //Make diagonal movement no longer move faster than normal
        velocity = inputDirection.normalized;
        //Add the speed to velocity
        velocity *= speed;
	}

	void FixedUpdate()
	{
		//Moving the player using velocity has get
		Rigidbody.MovePosition(Rigidbody.position + velocity * Time.fixedDeltaTime);
	}

	void LateUpdate()
	{
		//Prevent player from moving out side map scale
		transform.position = new Vector2
		(
			Mathf.Clamp(transform.position.x, -mapScale.x, mapScale.x), 
			Mathf.Clamp(transform.position.y, -mapScale.y, mapScale.y)
		);
		//Player position and use it as default destination
		Vector2 pos = transform.position; Vector2 destination = new Vector2(pos.x, pos.y);
		//The camera hitbox size
		Vector2 hitbox = new Vector2(camScale.x/1.05f, camScale.y/1.05f);
		//Stop destination in an axis when camera hitbox touch the border
		if(pos.x + hitbox.x/2 >= +mapScale.x) destination.x = cam.transform.position.x;
		if(pos.x - hitbox.x/2 <= -mapScale.x) destination.x = cam.transform.position.x;
		if(pos.y + hitbox.y/2 >= +mapScale.y) destination.y = cam.transform.position.y;
		if(pos.y - hitbox.y/2 <= -mapScale.y) destination.y = cam.transform.position.y;
		//Make the camera follow destination 
		cam.transform.position = new Vector3(destination.x, destination.y, -10);
	}
}
