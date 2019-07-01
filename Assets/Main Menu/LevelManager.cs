using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void loadlevel(string name)
	{
		Debug.Log ("Level Load requested: "+name);
		//Application.LoadLevel(name);
		SceneManager.LoadScene(name);
	}
	
	public void quit()
	{
		Debug.Log ("Level Load requested: "+name);	
		Application.Quit();
	}
}
