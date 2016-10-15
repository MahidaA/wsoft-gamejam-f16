using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Enemy))]
public class StandardGuard : MonoBehaviour {

	[HideInInspector]
	public Waypoint guardLoc;
	private Enemy e;

	void Start(){
		e=GetComponent<Enemy>();
	}

	void Update () {
		if(guardLoc==null){
			guardLoc=e.start;
			e.guard(guardLoc);
		}
			
		if(e.isDistracted()){
			e.acceptDistraction();
		}

		if(e.isEngaging() && e.distractionFinished()){
			e.guard(guardLoc);
		}

	}
}
