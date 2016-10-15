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
        grounded = false;
	}

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, GetComponent<Collider2D>().bounds.extents.y + 0.01f);
        if (hit.collider == null)
            return false;

        if (hit.transform.tag == "Platform" || hit.transform.tag == "pcol")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey("right"))
        {
            transform.Translate(speed * Vector2.right * Time.deltaTime);
            faceRight = true;
        }

        if (Input.GetKeyDown("space") && isGrounded())
        {
            rb.velocity = Vector2.up * jumpSpeed;
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
            Input.GetKey("right") && !isGrounded())
            {
                transform.Translate(Vector2.up * 2 * Time.deltaTime);
                transform.Translate(Vector2.right * Time.deltaTime);
            }

            if (other.gameObject.transform.position.x <= transform.position.x &&
            Input.GetKey("left") && !isGrounded())
            {
                transform.Translate(Vector2.up * 2 * Time.deltaTime);
                transform.Translate(Vector2.left * Time.deltaTime);
            }
        }
    }
 
}
