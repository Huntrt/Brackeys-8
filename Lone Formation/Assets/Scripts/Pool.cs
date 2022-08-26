using System.Collections.Generic; 
using UnityEngine;

public class Pool : MonoBehaviour
{
	//Set this class to singleton
	public static Pool i {get{if(_i==null){_i = GameObject.FindObjectOfType<Pool>();}return _i;}} static Pool _i;
	//The pool contain all the object has create
	public List<GameObject> poolList = new List<GameObject>();

	//Create the object needed with wanted position, rotation, does it auto active upon create? and do it need to has parent?
    public GameObject Create(GameObject Need, Vector3 Position, Quaternion Rotation, bool AutoActive = true, Transform Parent = null)
    {
		///If there is unactive object in pool
        if(poolList.Count > 0)
		{
			//Go through all the object in pool in reverse order
			for (int i = poolList.Count-1; i >= 0; i--)
			{
				//Remove any null object left from pool then skip it from being check
				if(poolList[i] == null) {poolList.RemoveAt(i); continue;}
				//If there is an unactive object in pool has the same name of object need to get
				if(!poolList[i].activeInHierarchy && poolList[i].name.Equals(Need.name+"(Clone)"))
				{
					//Set the polled object position
					poolList[i].transform.position = Position;
					//Set the polled object rotation
					poolList[i].transform.rotation = Rotation;
					//Set the pooled object parent from parameter
					poolList[i].transform.SetParent(Parent);
					//Active it depend if need to active
					poolList[i].SetActive(AutoActive);
					//Return it that object and no need to create need
					return poolList[i];
				}
			}
		}
		///If there is no unactive object left in pool
		{
			//Create the needed object witth set position and rotation
			GameObject newObject = Instantiate(Need, Position, Rotation);
			//Set the new object parent from parameter
			newObject.transform.SetParent(Parent);
			//Add it into pool list
			poolList.Add(newObject);
			//Set the new object active state
			newObject.SetActive(AutoActive);
			//Return the new object to
			return newObject;
		}
    }
}