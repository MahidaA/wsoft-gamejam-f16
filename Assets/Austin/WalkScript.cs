using UnityEngine;
using System.Collections;

public class WalkScript : MonoBehaviour {

	public float speed;
	public float climbSpeed;
	public bool walking;
	Animator animator;
	public bool goingUp;
	public int level;
	public bool climbing;

	// Use this for initialization
	void Start () {
		walking = false;
		animator = GetComponent<Animator>();
	}

	public void ladderSwitch(Vector3 pos){
		gameObject.transform.position = pos;
		if(climbing){
			
			gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
		}
		else{
			gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
		}
		climbing = !climbing;
		Debug.Log("switch!");
		animator.SetBool("climbing", climbing);
	}

	// Update is called once per frame
	void Update () {
		if(climbing){
			if(goingUp){
				gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+climbSpeed,0);
			}
			else{
				gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-climbSpeed,0);
			}
		}
		else{
			if(Input.GetKey(KeyCode.RightArrow)){
				if(!walking){
					walking = true;
					animator.SetBool("walking", walking);
				}
				gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
				gameObject.transform.position = new Vector3(gameObject.transform.position.x+speed,gameObject.transform.position.y,0);
			}
			else if(Input.GetKey(KeyCode.LeftArrow)){
				if(!walking){
					walking = true;
					animator.SetBool("walking", walking);
				}
				gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
				gameObject.transform.position = new Vector3(gameObject.transform.position.x-speed,gameObject.transform.position.y,0);
			}
			else{
				if(walking){
					walking = false;
					animator.SetBool("walking",walking);
				}
			}
		}
	}
}
