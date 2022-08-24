using UnityEngine;
using System;
public class Formation : MonoBehaviour
{
	public Vector2 spacing;
	[Serializable] public class FormationAxis {public float row, column;} public FormationAxis axis;
	public GameObject goalGrouper, goalPrefab;
	public Transform[] goals;
	public event Action formationed;

	void Start()
	{
		CreateGoal();
	}

	public void CreateGoal()
	{
		for (int g = 0; g < goals.Length; g++) {Destroy(goals[g].gameObject);}
		int amount = Leader.i.allies.Count;
		goals = new Transform[amount];
		Vector2[] initGoal = new Vector2[amount];

		/// Create grid
		Vector2 currentPos = Vector2.zero;
		//Count how many column has reach
		int columnReached = -1; 
		//Set column by flooring the square root of amount
		axis.column = Mathf.Floor(Mathf.Sqrt(amount));
		//Row has 1 by default
		axis.row = 1;
		//Go through all the goal
		for (int g = 0; g < amount; g++)
		{
			//Has reach another column
			columnReached++;
			//Spacing current in the X axis if this goal are not the first
			if(g != 0) currentPos.x += spacing.x;
			//If has reached max column
			if(columnReached == axis.column)
			{
				//Onto an new row and reset column reached
				axis.row++; columnReached = 0;
				//Reset current X axis to zero
				currentPos.x = 0;
				//Spacing current in the Y axis 
				currentPos.y += spacing.y;
			}
			//Set inital goal as current position has get
			initGoal[g] = currentPos;
		}

		/// Align grid
		for (int g = 0; g < initGoal.Length; g++)
		{
			//Align initial goal base on row and column
			initGoal[g].x -= spacing.x * axis.column;
			initGoal[g].y -= spacing.y * ((axis.row-1)/2);
			//Creating goal
			GameObject goal = Instantiate(goalPrefab, initGoal[g], Quaternion.identity);
			goal.transform.SetParent(goalGrouper.transform);
			goals[g] = goal.transform;
		}
		//Send event when complete goal formation
		formationed?.Invoke();
	}
}
