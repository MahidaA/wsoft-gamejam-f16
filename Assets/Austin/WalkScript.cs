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
	public bool inClimbZone;
	public bool isRight;
	public bool isClimbingLedge;
	bool okToClimb;
	public float ledgeWaitTime;
	public bool isJumping;
	bool jumpUp;
	int i;
	public bool isFalling;

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

	public void climbLedge(){
		isClimbingLedge = true;
		animator.SetBool("isClimbingLedge",isClimbingLedge);
     }

    public void fall(){
		animator.SetBool("isFalling",isFalling);
		isFalling = true;
    }

	// Update is called once per frame
	void Update () {
		Debug.Log("level is: "+level);
		if(climbing){
			if(goingUp){
				gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+climbSpeed,0);
			}
			else{
				gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-climbSpeed,0);
			}
		}
		else if(isJumping){
			if(i<10){
				i++;
			}
			else{
				if(jumpUp){
					gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+0.7f,0);
					if(inClimbZone){
						isJumping = false;
						climbLedge();
					}
				}
				else{
					gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-0.7f,0);
				}

				if(gameObject.transform.position.y > (level-1)*45 + 15){
					jumpUp = false;
				}
				if(gameObject.transform.position.y <= (level-1)*45){
					gameObject.transform.position = new Vector3(gameObject.transform.position.x, (level-1)*45, 0);
					isJumping = false;
				}
			}
		}
		else if(isClimbingLedge){
			float addX = -0.05f;
			if(isRight){
				addX = 0.05f;
			}
			gameObject.transform.position = new Vector3(gameObject.transform.position.x+addX,gameObject.transform.position.y+0.7f,0);
			if(gameObject.transform.position.y >= (level)*45){
				level += 1;
				isClimbingLedge = false;
				animator.SetBool("isClimbingLedge",isClimbingLedge);
			}
		}
		else if(isFalling){
			gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-1.5f,0);
//			Debug.Log(gameObject.transform.position.y+"  "+(level-2)*45);
			if(gameObject.transform.position.y <= (level-2)*45){
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, (level-2)*45, gameObject.transform.position.z);
				level -= 1;
				isFalling = false;
				animator.SetBool("isFalling",isFalling);
			}
		}
		else{
			if(Input.GetKey(KeyCode.Space)){
				animator.SetTrigger("jump");
				isJumping = true;
				jumpUp = true;
				i = 0;
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				if(!walking){
					walking = true;
					animator.SetBool("walking", walking);
				}
				isRight = true;
				gameObject.transform.eulerAngles = new Vector3(0, 270, 0);
				gameObject.transform.position = new Vector3(gameObject.transform.position.x+speed,gameObject.transform.position.y,0);
			}
			else if(Input.GetKey(KeyCode.LeftArrow)){
				if(!walking){
					walking = true;
					animator.SetBool("walking", walking);
				}
				isRight = false;
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
