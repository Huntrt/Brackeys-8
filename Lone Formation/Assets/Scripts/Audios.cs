using UnityEngine;

public class Audios : MonoBehaviour
{
	//Set this class to singleton
	public static Audios i {get{if(_i==null){_i = GameObject.FindObjectOfType<Audios>();}return _i;}} static Audios _i;

    [SerializeField] AudioClip alliesHurt, alliesDie;
    [SerializeField] AudioClip enemyHurt, enemyDie;
	[SerializeField] AudioClip gameOver;

	public void alliesHurtPlay() => PlaySound(alliesHurt);
	public void alliesDiePlay() => PlaySound(alliesDie);
	public void enemyHurtPlay() => PlaySound(enemyHurt);
	public void enemyDiePlay() => PlaySound(enemyDie);
	public void gameOverPlay() => PlaySound(gameOver);

	void PlaySound(AudioClip sound) => GameManager.i.audioM.soundSource.PlayOneShot(sound);
}
