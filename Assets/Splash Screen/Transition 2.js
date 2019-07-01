#pragma strict

var timer: float = 10.0;

function Update()
{
	timer -= Time.deltaTime;
	
	if(timer<=0)
	{
		//Application.LoadLevel("Menu");
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}