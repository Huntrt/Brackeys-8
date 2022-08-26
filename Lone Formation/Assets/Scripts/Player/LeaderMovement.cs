using UnityEngine;

public class LeaderMovement : MonoBehaviour
{
	Vector3 inputDirection;
	public Rigidbody2D Rigidbody;
	[SerializeField] Camera cam;
	Vector2 mapScale, camScale;

	void Start()
	{
		mapScale = Map.i.scale;
		//Get camera size
		camScale.y = 2 * cam.orthographicSize;
		camScale.x = camScale.y * cam.aspect;
	}
	
    void Update()
    {
		//Running function
		MoveInput();
	}

	void FixedUpdate()
	{
		//Moving the player using velocity has get
		Rigidbody.MovePosition(Rigidbody.position + velocity * Time.fixedDeltaTime);
	}

	void LateUpdate()
	{
		//Prevent player from moving outside map scale
		transform.position = new Vector2
		(
			Mathf.Clamp(transform.position.x, -mapScale.x, mapScale.x), 
			Mathf.Clamp(transform.position.y, -mapScale.y, mapScale.y)
		);
		CameraFollow();
	}

	Vector2 velocity; void MoveInput()
	{
		//Using binded key to use for input direction 
		KeyManager key = KeyManager.i;
		inputDirection = Vector2.zero;
		if(Input.GetKey(key.Up))    inputDirection.y = +1;
		if(Input.GetKey(key.Down))  inputDirection.y = -1;
		if(Input.GetKey(key.Left))  inputDirection.x = -1;
		if(Input.GetKey(key.Right)) inputDirection.x = +1;
		//@ Prevent movement got slow when hug border
		Vector2 pos = transform.position;
		if(inputDirection.x == 01 && pos.x >= +mapScale.x) inputDirection.x = 0;
		if(inputDirection.x == -1 && pos.x <= -mapScale.x) inputDirection.x = 0;
		if(inputDirection.y == 01 && pos.y >= +mapScale.y) inputDirection.y = 0;
		if(inputDirection.y == -1 && pos.y <= -mapScale.y) inputDirection.y = 0;
        //Make diagonal movement no longer move faster than normal
        velocity = inputDirection.normalized;
        //Add the speed to velocity
        velocity *= Leader.i.stats.movementSpeed;
	}

	void CameraFollow()
	{
		//Player position and use it as default destination
		Vector2 pos = transform.position; Vector2 destination = pos;
		//Get the camera view by using half of camera size that got decrease with leader size
		Vector2 view = (camScale - (Vector2)Leader.i.transform.localScale) /2;
		//Get snap position to by decrease the map scale with view
		Vector2 snap = mapScale - view;
		//@ Snap camera back to map when the view touch the map border
		if(pos.x + view.x >= +mapScale.x) destination.x = +snap.x;
		if(pos.x - view.x <= -mapScale.x) destination.x = -snap.x;
		if(pos.y + view.y >= +mapScale.y) destination.y = +snap.y;
		if(pos.y - view.y <= -mapScale.y) destination.y = -snap.y;
		//Make the camera follow destination 
		cam.transform.position = new Vector3(destination.x, destination.y, -10);
	}
}
