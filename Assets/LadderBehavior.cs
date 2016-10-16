using UnityEngine;
using System.Collections;

public class LadderBehavior : MonoBehaviour {

	public WalkScript playerScript;
	public int level1;
	public int level2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		if(!playerScript.climbing){
			if(Input.GetKey(KeyCode.UpArrow)){
				
				if(playerScript.level == level1){
					playerScript.ladderSwitch(new Vector3(gameObject.transform.position.x, (playerScript.level-1)*45,0));
					playerScript.goingUp = true;
					playerScript.level += 2;
				}
				else if(playerScript.level == level2){
					playerScript.ladderSwitch(new Vector3(gameObject.transform.position.x, (playerScript.level-1)*45,0));
					playerScript.goingUp = false;
					playerScript.level -= 2;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if(playerScript.climbing){
			if(playerScript.level == level2){ // got to top
				playerScript.ladderSwitch(new Vector3(gameObject.transform.position.x, 91,0));
			}
			else{
				playerScript.ladderSwitch(new Vector3(gameObject.transform.position.x, 0,0));
			}
		}
    }
}
