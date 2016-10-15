using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public List<Waypoint> waypoints;
	public Waypoint start, end;
	public float speed=2;

	public List<Waypoint> path;
	int pathLoc;
	private Waypoint current, next;

	private bool walking;

	void Start () {
		transform.position=start.transform.position;
		current=start;
		calculatePath(end);
		go();
	}
	
	// Update is called once per frame
	void Update () {
		if(walking){
			if(path.Count==0)
				return;
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

	public void stop(){
		walking=false;
	}

	public void go(){
		walking=true;
		next=path[0];
	}

	public void calculatePath(Waypoint newGoal){
		stop();
		path=recursiveSearch(current, newGoal, new List<Waypoint>());
		if(!path.Contains(next)){
			path.Insert(0, current);
		}
		if(Vector3.Distance(transform.position, path[0].transform.position)<1){
			path.RemoveAt(0);
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
				Debug.Log("GOING IN, "+w);
				paths[i++]=recursiveSearch(w, goal, search);
				Debug.Log("GOING OUT, "+w);
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
