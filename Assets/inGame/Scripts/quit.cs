using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour {

	public void quitout()
	{
		Debug.Log ("Level Load requested: "+name);	
		Application.Quit();
	}
}
