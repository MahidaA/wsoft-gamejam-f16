using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Distraction))]
public class BreakOnKey : MonoBehaviour {

	public string keyName;

	void Update(){
		if(Input.GetKeyDown(keyName)){
			GetComponent<Distraction>().distract();
		}
	}
}
