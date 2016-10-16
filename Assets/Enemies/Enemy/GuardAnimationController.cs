using UnityEngine;
using System.Collections;

public class GuardAnimationController : MonoBehaviour {

	private Vector3 lastPos;
	private Animator anim;

	void Start(){
		lastPos=transform.position;
		anim=GetComponent<Animator>();
	}


	void Update () {
		Vector3 speed=(lastPos-transform.position)/Time.deltaTime;
		bool walking=false;
		bool climbing=false;
		if(speed.sqrMagnitude>0.8){
			if(Vector3.Angle(Vector3.right, speed) < 80){
				transform.eulerAngles=new Vector3(0, 90, 0);
				walking=true;
			}else if(Vector3.Angle(Vector3.left, speed) < 80){
				transform.eulerAngles=new Vector3(0, 270, 0);
				walking=true;
			}else{
				transform.eulerAngles=new Vector3(0, 180, 0);
				climbing=true;
			}
		}
		anim.SetBool("walking", walking);
		anim.SetBool("climbing", climbing);
			

		lastPos=transform.position;
	}
}
