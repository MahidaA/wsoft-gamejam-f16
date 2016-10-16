using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Distraction))]
public class MakeNoise : MonoBehaviour {

	bool state; //true if someone is within the trigger
	private Player p;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e") && state)
		{
			
			StartCoroutine(waitThenDistract());
			p.disable(1.5F);
			p.GetComponent<PlayerAnimationController>().anim.SetTrigger("Interact");
		}
	}

	private IEnumerator waitThenDistract(){
		yield return new WaitForSecondsRealtime(1.5F);
		GetComponent<Distraction>().distract();
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
