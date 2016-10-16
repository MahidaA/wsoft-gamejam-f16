using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Credits : MonoBehaviour {


	void OnGUI(){
		if(GUI.Button(new Rect(Screen.width/5, Screen.height*8/10, Screen.width*3/5, Screen.height/6), "Back to Menu")){
			SceneManager.LoadScene("SceneMenu");
		}
	}
}
