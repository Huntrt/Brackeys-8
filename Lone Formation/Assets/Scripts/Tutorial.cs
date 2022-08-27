using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
	[SerializeField] float showDuration;
    public GameObject[] texts;
	int currentTexts;

	void Start()
	{
		//Show the first tutorial
		ShowText();
	}

	void ShowText()
	{
		if(currentTexts >= texts.Length) {Destroy(gameObject); return;}
		//Show text then shelf destruct itslf when out of text
		texts[currentTexts].SetActive(true);
		if(currentTexts != 0) texts[currentTexts-1].SetActive(false);
		currentTexts++;
		Invoke("ShowText", showDuration);
	}
}