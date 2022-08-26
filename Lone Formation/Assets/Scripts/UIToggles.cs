using UnityEngine;

public class UIToggles : MonoBehaviour
{
	public void Toggle(GameObject obj)
	{
		obj.SetActive(!obj.activeInHierarchy);
	}
} 