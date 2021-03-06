﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Distraction))]
public class MakeVisualDistraction : MonoBehaviour {

	bool state; //true if someone is within the trigger
	private Player p;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e") && state)
		{
			GetComponent<Distraction>().isVisualDistraction=true;
			GetComponent<Renderer>().material.color=Color.green;
//			p.GetComponent<PlayerAnimationController>().anim.SetTrigger("Interact");
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
