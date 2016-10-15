using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GuardThenPatrol))]
public class BeginPatrolAfterNoise : MonoBehaviour {
	
	void Update () {
		if(GetComponent<Enemy>().isEngaging()){
			GetComponent<GuardThenPatrol>().guarding=false;
		}
	}
}
