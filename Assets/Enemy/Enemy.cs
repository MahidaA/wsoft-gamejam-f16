﻿using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public const float PATROL_SPEED=3, RUN_SPEED=10;

	public enum EnemyState{
		PATROLLING, ENGAGING
	}
	public EnemyState state;


	public List<Waypoint> waypoints;

	//Default starting point
	public Waypoint start;

	public float speed=2;

	//For hearing noises
	[SerializeField]
	public float distractionTime;

	//For patroling
	public Waypoint[] patrolPoints;
	public float[] waitTimes;
	private int patrolPointIndex;
	private float waitTime;
	private bool shouldWait;


	//For going to specific points
	private List<Waypoint> path;
	private Waypoint current, next;
	private bool walking;

	void Start () {
		transform.position=start.transform.position;
		state=EnemyState.PATROLLING;
		speed=PATROL_SPEED;
		patrolPointIndex=0;
		current=start;
		shouldWait=false;
	}
	
	// Update is called once per frame
	void Update () {

		if(state==EnemyState.PATROLLING){
			if(path==null||path.Count==0){

				waitTime+=Time.deltaTime;

				if(!shouldWait || waitTime>=waitTimes[patrolPointIndex]){
					shouldWait=true;
					waitTime=0;
					patrolPointIndex++;
					patrolPointIndex%=patrolPoints.Length;
					calculatePath(patrolPoints[patrolPointIndex]);
					go();
				}
			}

		}

		if(state==EnemyState.ENGAGING){
			//Enemy at the final waypoint
			if(path.Count==0){
				distractionTime-=Time.deltaTime;
				if(distractionTime<=0){
					state=EnemyState.PATROLLING;
					speed=PATROL_SPEED;
					shouldWait=false;
				}
			}
		}

		if(walking){
			if(path.Count==0){
				walking=false;
			}else{
				if(Vector3.Distance(transform.position, path[0].transform.position)<speed*Time.deltaTime){
					transform.position=next.transform.transform.position;
					path.RemoveAt(0);
					current=next;
					if(path.Count==0){
						stop();
						return;
					}
					next=path[0];
				}else{
					transform.position+=(next.transform.position-transform.position).normalized*speed*Time.deltaTime;
				}
			}
		}


	}

	public void stop(){
		walking=false;
	}

	public void go(){
		walking=true;
		next=path[0];
	}

	public void calculatePath(Waypoint newGoal){
		stop();
		if(current==newGoal||next==newGoal){
			path=new List<Waypoint>();
			path.Add(newGoal);
			next=newGoal;
			return;
		}
		
		path=recursiveSearch(current, newGoal, new List<Waypoint>());
		if(!path.Contains(next)){
			path.Insert(0, current);
		}

		next=path[0];
	}

	private List<Waypoint> recursiveSearch(Waypoint now, Waypoint goal, List<Waypoint> search){
		search=new List<Waypoint>(search);
		List<Waypoint>[] paths=new List<Waypoint>[now.connected.Count];
		int i=0;
		foreach(Waypoint w in now.connected){
			if(search.Contains(w))
				paths[i++]=null;
			else if(w==goal){
				search.Add(w);
				return search;
			}else{
				search.Add(w);
				paths[i++]=recursiveSearch(w, goal, search);
				search.Remove(w);
			}
		}

		int maxLen=int.MaxValue, maxInd=-1;
		for(i=0;i<paths.Length;i++){
			if(paths[i]==null)
				continue;
			else if(paths[i].Count<maxLen){
				maxLen=paths[i].Count;
				maxInd=i;
			}
		}

		if(maxInd==-1)
			return null;
		return paths[maxInd];
	}
}
