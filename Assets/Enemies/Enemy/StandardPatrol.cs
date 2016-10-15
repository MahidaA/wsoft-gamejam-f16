using UnityEngine;
using System.Collections;

public class StandardPatrol : MonoBehaviour {

	public Waypoint[] waypoints;
	public float[] waitTimes;

	private Enemy e;

	void Start () {
		e=GetComponent<Enemy>();
	}

	void Update () {

		if(e.isDistracted()){
			e.acceptDistraction();
		}

		if(!e.hasPath() || (e.isEngaging() && e.distractionFinished())){
			e.patrol(waypoints, waitTimes);
		}
	}
}
