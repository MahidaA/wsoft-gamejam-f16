using UnityEngine;
using System.Collections;

public class EnemyAlert : MonoBehaviour {

	public GuardThenPatrol otherGuard;

	void Update(){
		if(GetComponent<Enemy>().isEngaging() && !GetComponent<Enemy>().isWalking()){
			otherGuard.guarding=false;
			Debug.Log(otherGuard.GetComponent<Enemy>().getClosest(transform.position));
			otherGuard.GetComponent<Enemy>().engage(otherGuard.GetComponent<Enemy>().getClosest(transform.position), 3);
		}
	}
}
