using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneMenu : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(Screen.width/5, Screen.height*7/10, Screen.width*3/5, Screen.height/5), "Begin Game")){
			SceneManager.LoadScene("scene_level1");
		}

	}
}
