using UnityEngine;
using System.Collections;

[RequireComponent (typeof(StandardPatrol))]
public class ModifyPatrolPathOnNoise : MonoBehaviour {

	public Waypoint[] replacementWaypoint;
	public float[] replacementTimes;

	void Update(){
		if(GetComponent<Enemy>().isEngaging() && !GetComponent<Enemy>().isWalking()){
			GetComponent<Enemy>().patrol(replacementWaypoint, replacementTimes);
			this.enabled=false;
		}
	}
}
