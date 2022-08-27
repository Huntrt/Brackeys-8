using UnityEngine;

public class CasterPoints : Caster
{
    public Transform[] points;

	public override void SetPattern()
	{
		//Create attack for all the point using it rotation and position
		for (int p = 0; p < points.Length; p++)
		{
			CreateAttack(points[p].position, points[p].rotation);
		}
	}
}
