using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {
	public GameObject target;

	// Use this for initialization
	void Start () {
		transform.LookAt(target.transform);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+50,-300);

	}
}
