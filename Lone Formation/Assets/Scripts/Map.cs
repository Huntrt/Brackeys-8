using UnityEngine;

public class Map : MonoBehaviour
{
	//Set this class to singleton
	public static Map i {get{if(_i==null){_i = GameObject.FindObjectOfType<Map>();}return _i;}} static Map _i;

    public Vector2 scale;
	[SerializeField] BorderSetting border; [System.Serializable] public class BorderSetting
	{
		public GameObject top, bot;
		public float length;
	}
	public ParticleSystem starParticle;

	void Start()
	{
		SetMapLength(scale.x);
		SetMapHeight(scale.y);
	}

	public void SetMapLength(float value)
	{
		scale.x = value;
		border.top.transform.localScale = new Vector2(value, 1);
		border.bot.transform.localScale = new Vector2(value, 1);
		//Make the star particle life time scale with map length and push it back
		var starLife = starParticle.main; starLife.startLifetime = scale.x;
		starParticle.transform.position = new Vector2(value + value/0.75f, 0);
	}

	public void SetMapHeight(float value)
	{
		scale.y = value;
		border.top.transform.position = new Vector2(0, value);
		border.bot.transform.position = new Vector2(0, -value);
		//Set the width of star paritcle
		starParticle.transform.localScale = new Vector2(1, value*2);
	}
}
