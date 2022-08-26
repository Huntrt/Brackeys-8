using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOperator : MonoBehaviour
{
	[Header("Gameplay")]
	public bool paused;
	public GameObject pauseMenu;

	//Set this class to singleton
	public static GameOperator i {get{if(_i==null){_i = GameObject.FindObjectOfType<GameOperator>();}return _i;}} static GameOperator _i;
	
	public void PauseToggle()
	{
		//Toggle the pause menu
		pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
		//Toggle pause
		paused = !paused;
		//Change game time pause on if it currently pause of not
		if(paused) Time.timeScale = 0; else Time.timeScale = 1;
	}

	void Update()
	{
		//Paue when press escaped
		if(pauseMenu != null && Input.GetKeyDown(KeyCode.Escape)) PauseToggle();
	}

    public void LoadSceneIndex(int i) {SceneManager.LoadScene(i, LoadSceneMode.Single);}
    public void QuitGame()  {Application.Quit();}
}