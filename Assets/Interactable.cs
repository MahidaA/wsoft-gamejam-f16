using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    //This should encompass doors, sewer grates and whatever else you can open/close

    bool state; //true if someone is within the trigger
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
