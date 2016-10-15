using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int speed = 3;
    public int jumpSpeed = 5;
    Rigidbody2D rb;
    bool faceRight;
    bool grounded;

	// Use this for initialization
	void Start () {
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ledge")
        {
            if (other.gameObject.transform.position.x >= transform.position.x &&
            Input.GetKey("right") && !grounded)
            {
                transform.Translate(Vector2.up * 2 * Time.deltaTime);
                transform.Translate(Vector2.right * Time.deltaTime);
            }

            if (other.gameObject.transform.position.x <= transform.position.x &&
            Input.GetKey("left") && !grounded)
            {
                transform.Translate(Vector2.up * 2 * Time.deltaTime);
                transform.Translate(Vector2.left * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "pcol")
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "pcol")
        {
            grounded = false;
        }
    }
}
