using UnityEngine;
using System.Collections;

public class SecurityTerminal : MonoBehaviour {

	public SecurityCamera cam;
	private Player p;
	private bool state; //true if someone is within the trigger

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e") && state)
		{
			cam.active=false;
			p.GetComponent<PlayerAnimationController>().anim.SetTrigger("Interact");
			p.disable(1.5F);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			state = true;
			p=other.GetComponent<Player>();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			state = false;
		}
	}

}
