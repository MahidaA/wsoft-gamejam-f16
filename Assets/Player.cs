using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int speed;
    public int jumpSpeed;
    Rigidbody2D rb;
    bool faceRight;
    bool grounded;

	// Use this for initialization
	void Start () {
        speed = 3;
        jumpSpeed = 5;
        rb = GetComponent<Rigidbody2D>();
        faceRight = true;
        grounded = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey("right"))
        {
            transform.Translate(speed * Vector2.right * Time.deltaTime);
            faceRight = true;
        }

        if (Input.GetKeyDown("up") && grounded)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            grounded = false;
        }

        if (Input.GetKey("left"))
        {
            transform.Translate(speed * Vector2.left * Time.deltaTime);
            faceRight = false;
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "pcol")
        {
            grounded = true;
        }

        if (coll.gameObject.tag == "Ledge")
        {
            //if (coll.gameObject.transform.position.x > transform.position.x && 
            //    Input.GetKey("Right") && !grounded)
            //{
            transform.Translate(Vector2.up * 10 * Time.deltaTime);
            transform.Translate(Vector2.right * 5 * Time.deltaTime);
            //}
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "pcol")
        {
            grounded = false;
        }
    }
}
