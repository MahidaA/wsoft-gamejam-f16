using UnityEngine;
using System.Collections;

public class FOV : MonoBehaviour {

	public Enemy enemy;

	void OnTriggerEnter2D(Collider2D col){

		Vector3 dir=col.transform.position-transform.position;
		if(col.transform.tag=="Player"){
			RaycastHit2D hit=Physics2D.Raycast(transform.position, col.transform.position-transform.position);
			if(hit.transform.tag=="Player"){
				GameObject.FindObjectOfType<Game>().gameOver();
			}
		}

		if(enemy!=null){
			if(col.GetComponent<Distraction>() && col.GetComponent<Distraction>().isVisualDistraction){
				RaycastHit2D hit=Physics2D.Raycast(transform.position, col.transform.position, dir.magnitude);
				Debug.Log(hit.collider);
				if(hit.collider==null || hit.collider==col){
					Waypoint closest=null;
					float dist=0;
					foreach(Waypoint w in enemy.getWaypoints()){
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

					enemy.seeDistraction(col.GetComponent<Distraction>(), closest);
				}
			}
		}
	}
}
