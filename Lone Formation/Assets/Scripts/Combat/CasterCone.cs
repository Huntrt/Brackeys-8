using UnityEngine;

public class CasterCone : Caster
{
    public int amount;
	public float spread;

	public override void SetPattern()
	{
		//Range are the focus stats got divide by 2 since it affect 2 direction
		float range = spread / 2;
		//Get the 180 to -180 rotation of the anchor
		float rot = transform.localEulerAngles.z; float center = (rot > 180) ? rot-360 : rot;
		//Get the start and end rotation by decrease and increase the center with range
		float start = center - range; float end = center + range;
		//Get the length between each step on the spread stat
		//-1 amount cuase has 1 extra than step, e.g: A = attack | s = step | A <-s-> A <-s-> A
		float step = spread / (amount-1);
		//Begin the frist angle at start
		float angle = start;
		//For each of the attack need to create
		for (int i = amount - 1; i >= 0 ; i--)
		{
			//Create attack with the rotation has get and at this position
			CreateAttack(transform.position, Quaternion.Euler(0,0,angle));
			//Proceed to the next step
			angle += step;
		}
	}
}
