using UnityEngine;
using TMPro;

public class Economic : MonoBehaviour
{
    //Set this class to singleton
	public static Economic i {get{if(_i==null){_i = GameObject.FindObjectOfType<Economic>();}return _i;}} static Economic _i;
	public int money;
	public TextMeshProUGUI counter;

	void Start() {Earn(0);}

	//Return bool base on if there still money to spend
	public bool Spended(int price, bool bought)
	{
		if(money >= price)
		{
			//Check if this is use for buying or just checking
			if(bought)
			{	
				money -= price;
				UpdateUI();
			}
			return true;
		}
		return false;
	}

	public void Earn(int earned)
	{
		money += earned;
		UpdateUI();
	}

	void UpdateUI()
	{
		counter.text = "$" + money;
	}
}