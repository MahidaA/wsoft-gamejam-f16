using UnityEngine;
using System.Collections;

public class BackgroundMovement : MonoBehaviour {

	public float mySpeed;

	private Vector2 savedOffset;
	// Use this for initialization
	void Start () {
		savedOffset = gameObject.GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.D)){
			Vector2 offset = gameObject.GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
			Vector2 setOffset = new Vector2 (Mathf.Repeat(offset.x,1) + mySpeed, offset.y);
			gameObject.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex",setOffset);
		}
		if(Input.GetKey(KeyCode.A)){
			Vector2 offset = gameObject.GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
			Vector2 setOffset = new Vector2 (Mathf.Repeat(offset.x,1) - mySpeed, offset.y);
			gameObject.GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex",setOffset);
		}
	}
}
