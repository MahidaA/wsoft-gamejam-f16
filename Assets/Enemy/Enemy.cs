using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {


	private enum EnemyState{
		PATROLLING, ENGAGING, GUARD
	}
	private EnemyState state;

	public const float PATROL_SPEED=3, RUN_SPEED=5;
	private float speed=2;

	private List<Waypoint> waypoints;

	//Default starting point
	public Waypoint start;

	//For hearing noises
	private Distraction distractedBy;
	private Waypoint distractionPoint;
	private float distractionTime;

	//For patroling
	private Waypoint[] patrolPoints;
	private float[] waitTimes;
	private int patrolPointIndex;
	private float waitTime;
	private bool shouldWait;

	//For guarding
	private Waypoint guardLocation;


	//For going to specific points
	private List<Waypoint> path;
	private Waypoint current, next;
	private bool walking;

	/// <summary>
	/// For viewing cone
	/// </summary>
	public GameObject FOV;
	private Vector3 prevPos;
	private float currentAngle;

	void Start () {
		transform.position=start.transform.position;
		speed=PATROL_SPEED;
		patrolPointIndex=0;
		current=start;
		shouldWait=false;

		FOV=(GameObject)GameObject.Instantiate(FOV);

		currentAngle=FOV.transform.eulerAngles.z;

		waypoints=new List<Waypoint>();
		addWaypoints(start);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 deltaPos=transform.position-prevPos;
		if(deltaPos!=Vector3.zero){
			float targetAngle=Vector3.Angle(Vector3.right, deltaPos)*Mathf.Sign(Vector3.Cross(Vector3.right, deltaPos).z);
			Debug.Log(targetAngle);
			currentAngle=Mathf.MoveTowardsAngle(currentAngle, targetAngle, 120*speed*Time.deltaTime);
			FOV.transform.position=transform.position+Vector3.up/4;
			FOV.transform.eulerAngles=new Vector3(currentAngle,-90,0);
			FOV.GetComponent<Renderer>().enabled=(currentAngle==targetAngle);
		}


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
			if(path.Count==0 && distractionTime>0){
				distractionTime-=Time.deltaTime;
			}
		}


		prevPos=transform.position;

		if(walking){
			if(path.Count==0){
				stop();
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

	public List<Waypoint> getWaypoints(){
		return waypoints;
	}

	public void patrol(Waypoint[] points, float[] waits){
		patrolPoints=points;
		waitTimes=waits;
		speed=PATROL_SPEED;
		state=EnemyState.PATROLLING;
		go();
	}

	public void guard(Waypoint guardLoc){
		guardLocation=guardLoc;
		speed=PATROL_SPEED;
		state=EnemyState.GUARD;
		calculatePath(guardLocation);
		go();
	}

	private void engageAt(Waypoint loc){
		speed=RUN_SPEED;
		state=EnemyState.ENGAGING;
		calculatePath(loc);
		go();
	}

	public void hearDistraction(Distraction d, Waypoint location){
		distractedBy=d;
		distractionPoint=location;
	}

	public bool hearingDistraction(){
		return distractedBy!=null;
	}

	public bool distractionFinished(){
		return distractionTime<=0;
	}

	public void acceptDistraction(){
		engageAt(distractionPoint);
		distractionTime=distractedBy.enemyDistractionTime;

		distractedBy=null;
		distractionPoint=null;
	}

	public bool isEngaging(){
		return state==EnemyState.ENGAGING;
	}

	private void stop(){
		walking=false;
	}

	private void go(){
		walking=true;
		next=path[0];
	}

	public void engage(Waypoint point, float enemyDistractionTime){
		
		if(state==EnemyState.GUARD){
			guardLocation=current;
		}

		calculatePath(point);
		speed=Enemy.RUN_SPEED;
		state=Enemy.EnemyState.ENGAGING;
		distractionTime=enemyDistractionTime;
		go();
	}

	private void calculatePath(Waypoint newGoal){
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

	private void addWaypoints(Waypoint w){
		if(!waypoints.Contains(w)){
			waypoints.Add(w);
			foreach(Waypoint p in w.connected)
				addWaypoints(p);
		}
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
