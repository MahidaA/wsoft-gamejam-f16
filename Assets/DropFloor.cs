using UnityEngine;
using System.Collections;

public class DropFloor : MonoBehaviour {

	void Update(){
		GetComponent<Collider2D>().isTrigger=Input.GetKey(KeyCode.DownArrow);
	}
}
