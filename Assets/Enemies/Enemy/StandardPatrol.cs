using UnityEngine;
using System.Collections;

public class StandardPatrol : MonoBehaviour {

	public Waypoint[] waypoints;
	public float[] waitTimes;

	private Enemy e;

	void Start () {
		e=GetComponent<Enemy>();
		Debug.Assert(waypoints.Length==waitTimes.Length, "Wait times must be the same size as the number of waypoints");
	}

	void Update () {

		if(e.isDistracted()){
			e.acceptDistraction();
		}

		if((!e.hasPath() || e.isEngaging()) && e.distractionFinished()){
			e.patrol(waypoints, waitTimes);
		}
	}
}
