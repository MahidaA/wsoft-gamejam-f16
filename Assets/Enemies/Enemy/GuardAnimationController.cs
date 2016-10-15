using UnityEngine;
using System.Collections;

public class GuardAnimationController : MonoBehaviour {

	private Vector3 lastPos;
	private Animator anim;
	public Enemy e;

	void Start(){
		lastPos=transform.position;
		anim=GetComponent<Animator>();
	}


	void Update () {
		if(Mathf.Abs(Mathf.DeltaAngle(180, e.FOV.transform.eulerAngles.z)) < 80){
			transform.eulerAngles=new Vector3(0, 90, 0);
		}else if(Mathf.Abs(Mathf.DeltaAngle(0, e.FOV.transform.eulerAngles.z)) < 80){
			transform.eulerAngles=new Vector3(0, 270, 0);
		}else{
			transform.eulerAngles=new Vector3(0, 180, 0);
		}
		Vector3 speed=(lastPos-transform.position)/Time.deltaTime;

		anim.SetBool("walking", (Mathf.Abs(speed.x)>0.3));

		anim.SetBool("climbing", Vector3.Angle(Vector3.up, speed)<10 || Vector3.Angle(Vector3.down, speed)<10);
			

		lastPos=transform.position;
	}
}
