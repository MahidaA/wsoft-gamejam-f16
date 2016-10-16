using UnityEngine;
using System.Collections;

public class LedgeScript : MonoBehaviour {

	public WalkScript playerScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(!playerScript.isJumping && !playerScript.isClimbingLedge){
			playerScript.fall();
		}
	}

	void OnTriggerStay(Collider other) {
		if((other.transform.position.x < gameObject.transform.position.x && playerScript.isRight) ||
		   (other.transform.position.x > gameObject.transform.position.x && !playerScript.isRight)){
			playerScript.inClimbZone = true;
			Debug.Log("YES");
		}
		else{
			playerScript.inClimbZone = false;
			Debug.Log("NO");
		}
	}
	void OnTriggerExit(Collider other){
		playerScript.inClimbZone = false;
	}
}
