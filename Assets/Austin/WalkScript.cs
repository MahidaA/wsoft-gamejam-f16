using UnityEngine;
using System.Collections;

public class WalkScript : MonoBehaviour {

	public float speed;
	public bool walking;
	Animator animator;


	// Use this for initialization
	void Start () {
		walking = false;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.RightArrow)){
			if(!walking){
				walking = true;
				animator.SetBool("walking", walking);
			}
			gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
			gameObject.transform.position = new Vector3(gameObject.transform.position.x+speed,0,0);
		}
		else if(Input.GetKey(KeyCode.LeftArrow)){
			if(!walking){
				walking = true;
				animator.SetBool("walking", walking);
			}
			gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
			gameObject.transform.position = new Vector3(gameObject.transform.position.x-speed,0,0);
		}
		else{
			if(walking){
				walking = false;
				animator.SetBool("walking",walking);
			}
		}
	}
}
