using UnityEngine;
using System.Collections;

[RequireComponent (typeof(StandardGuard))]
[RequireComponent (typeof(StandardPatrol))]
public class GuardThenPatrol : MonoBehaviour {

	private StandardGuard guard;
	private StandardPatrol patrol;

	public bool guarding;
	private bool last;

	void Start(){
		guard=GetComponent<StandardGuard>();
		patrol=GetComponent<StandardPatrol>();

		last=guarding;
		guard.enabled=guarding;
		patrol.enabled=!guarding;

		patrol.enabled=false;
	}

	void Update(){

		if(last!=guarding){
			guard.enabled=guarding;
			patrol.enabled=!guarding;

			if(guarding){
				GetComponent<Enemy>().guard(guard.guardLoc);
			}else{
				GetComponent<Enemy>().patrol(patrol.waypoints, patrol.waitTimes);
			}
		}

		last=guarding;
	}
}
