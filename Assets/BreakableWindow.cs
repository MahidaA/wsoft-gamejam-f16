﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Distraction))]
public class BreakableWindow : MonoBehaviour {

	private bool state=false;

	void Update () {
		if (Input.GetKeyDown("e") && state)
		{
			if(!GetComponent<Distraction>().isVisualDistraction){
				Distraction d=GetComponent<Distraction>();
				GetComponent<Renderer>().enabled=false;
				GetComponent<Collider>().isTrigger=true;
				d.isVisualDistraction=false;
				d.distract();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			state = true;
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