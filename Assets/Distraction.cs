﻿using UnityEngine;
using System.Collections;

public class Distraction : MonoBehaviour {

	public float soundRadius;
	public bool isVisualDistraction;
	public float enemyDistractionTime;


	void Start(){
		Debug.Assert(GetComponent<Distraction>().enemyDistractionTime!=0, "Enemy Distraction Time Cannot be 0");
	}

	public void distract(){
		foreach(Enemy e in FindObjectsOfType<Enemy>()){
			if(Vector3.Distance(transform.position, e.transform.position)<=soundRadius){
				Waypoint closest=null;
				float dist=0;
				foreach(Waypoint w in e.getWaypoints()){
					if(closest==null){
						closest=w;
						dist=Vector3.Distance(w.transform.position, transform.position);
					}else{
						float storeDist;
						if((storeDist=Vector3.Distance(w.transform.position, transform.position))<dist){
							closest=w;
							dist=storeDist;
						}
					}
				}

				e.hearDistraction(this, closest);
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		for(float theta=0;theta<Mathf.PI*2;theta+=Mathf.PI/8){
			Gizmos.DrawLine(transform.position, transform.position+
				soundRadius*new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)));
		}
	}
}
