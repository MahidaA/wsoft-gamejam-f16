using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    //This should encompass doors, sewer grates and whatever else you can open/close

    bool state; //true if someone is within the trigger
	private Player p;
    // Use this for initialization
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e") && state)
        {
            GetComponent<BoxCollider2D>().enabled = !(GetComponent<BoxCollider2D>().enabled);
            gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;

			if(GetComponent<Distraction>()!=null)
            	GetComponent<Distraction>().isVisualDistraction = !GetComponent<Distraction>().isVisualDistraction;

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
