using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    int speed;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        speed = 3;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey("right"))
        {
            transform.Translate(speed * Vector2.right * Time.deltaTime);
        }

        if (Input.GetKeyDown("up"))
        {
            rb.velocity = Vector2.up * 5;
        }

        if (Input.GetKey("left"))
        {
            transform.Translate(speed * Vector2.left * Time.deltaTime);
        }
	}
}
