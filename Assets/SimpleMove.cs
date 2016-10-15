using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

	public float mySpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.D)){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - mySpeed, gameObject.transform.position.y, gameObject.transform.position.z);
		}
		if(Input.GetKey(KeyCode.A)){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + mySpeed, gameObject.transform.position.y, gameObject.transform.position.z);
		}
	}
}
