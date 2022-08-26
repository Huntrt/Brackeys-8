using UnityEngine;

public class Audios : MonoBehaviour
{
	//Set this class to singleton
	public static Audios i {get{if(_i==null){_i = GameObject.FindObjectOfType<Audios>();}return _i;}} static Audios _i;

    public AudioClip alliesHit, alliesDie;
    public AudioClip enemyHit, enemyDie;
	public AudioClip gameOver;
}
