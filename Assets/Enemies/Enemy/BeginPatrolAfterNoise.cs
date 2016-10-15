using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GuardThenPatrol))]
public class BeginPatrolAfterNoise : MonoBehaviour {

	void Awake(){
		GetComponent<GuardThenPatrol>().guarding=true;
	}
	
	void Update () {
		if(GetComponent<Enemy>().isEngaging()){
			GetComponent<StandardGuard>().enabled=false;

			if(GetComponent<Enemy>().distractionFinished()){
				GetComponent<GuardThenPatrol>().guarding=false;
			}
		}
	}
}
