#pragma strict

var timer: float = 10.0;

function Update()
{
	timer -= Time.deltaTime;
	
	if(timer<=0)
	{
		//Application.LoadLevel("Splash2");
		UnityEngine.SceneManagement.SceneManager.LoadScene("Splash2");
	}
}