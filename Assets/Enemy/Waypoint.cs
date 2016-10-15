using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {
	public List<Waypoint> connected;

	void Awake(){
		GetComponent<Renderer>().enabled=false;
		foreach(Waypoint w in connected){
			if(!w.connected.Contains(this))
				w.connected.Add(this);
		}
	}

	void OnDrawGizmos(){
		foreach(Waypoint w in connected){
			Gizmos.DrawLine(transform.position, w.transform.position);
		}
	}
}
