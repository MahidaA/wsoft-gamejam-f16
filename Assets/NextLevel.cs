using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag=="Player")
			GameObject.FindObjectOfType<Game>().nextLevel();
	}
}
