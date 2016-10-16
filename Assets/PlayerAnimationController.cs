using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	public GameObject playerModel;
	private Player p;
	public Animator anim;

	public bool jumping;
	public bool ledge;

	void Start(){
		playerModel=Instantiate(playerModel);
		p=GetComponent<Player>();
		anim=playerModel.GetComponent<Animator>();

	}
		
	void LateUpdate () {
		if(p.ignore)
			return;
		playerModel.transform.position=transform.position+new Vector3(GetComponent<Collider2D>().bounds.extents.x, 0);
		anim.SetBool("grounded",p.grounded);
		anim.SetBool("isFalling",false);
		anim.SetBool("isClimbingLedge", false);
		if(p.onLadder){
			playerModel.transform.eulerAngles=new Vector3(0,180,0);
			anim.SetBool("climbing", true);
			if(Input.GetKey(KeyCode.UpArrow)){
				anim.speed=3;
			}else if(Input.GetKey(KeyCode.DownArrow)){
				anim.speed=-3;
			}else{
				anim.speed=0;
			}

		}else if(ledge){
			anim.SetBool("grounded",false);
			anim.SetBool("isFalling",false);

			anim.SetBool("isClimbingLedge", true);
		}else{
			anim.speed=1;
			anim.SetBool("climbing", false);
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				playerModel.transform.eulerAngles=new Vector3(0,90,0);
			}else if(Input.GetKeyDown(KeyCode.RightArrow)){
				playerModel.transform.eulerAngles=new Vector3(0,270,0);
			}

			if(p.grounded){
				anim.SetBool("walking", Input.GetKey(KeyCode.LeftArrow)^Input.GetKey(KeyCode.RightArrow));
			}else{
				if(jumping){
					anim.SetTrigger("jump");
					jumping=false;
				}else{
					anim.SetBool("isFalling",true);;
				}
			}
		}
	}
}
