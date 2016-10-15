﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int speed = 3;
    public int jumpSpeed = 5;
    Rigidbody2D rb;
    Collider2D coll;
    bool faceRight;
    public bool grounded;
    bool onLadder;
    int layermask;
    public bool onStairs;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        faceRight = true;
        grounded = true;
        layermask = 1 << 8;

        GameObject[] stairs = GameObject.FindGameObjectsWithTag("Stairs");
        foreach (var st in stairs)
        {
            st.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    void isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, coll.bounds.extents.y + 0.05f);

        RaycastHit2D staircheckr = Physics2D.Raycast(transform.position, -Vector2.up + Vector2.right, Mathf.Sqrt(Mathf.Pow(coll.bounds.extents.y, 2) + Mathf.Pow(coll.bounds.extents.x, 2)) + 0.5f, layermask);

        RaycastHit2D staircheckl = Physics2D.Raycast(transform.position, -Vector2.up + -Vector2.right, Mathf.Sqrt(Mathf.Pow(coll.bounds.extents.y, 2) + Mathf.Pow(coll.bounds.extents.x, 2)) + 0.5f, layermask);
        print(staircheckl.transform);

        if (staircheckr || staircheckl)
        {
            onStairs = true;
            grounded = true;
            return;
        }
        else
        {
            onStairs = false;
        }

        if (!hit)
        {
            grounded = false;
            return;
        }

        if (hit.transform.tag == "Platform" || hit.transform.tag == "pcol")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    // Update is called once per frame
    void Update () {
        isGrounded();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (onStairs)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if (onStairs && Input.GetKeyDown("down"))
        {
            GameObject[] stairs = GameObject.FindGameObjectsWithTag("Stairs");
            foreach (var st in stairs)
            {
                st.GetComponent<Collider2D>().isTrigger = true;
            }
        }

	    if (Input.GetKey("right"))
        {
            transform.Translate(speed * Vector2.right * Time.deltaTime);
            faceRight = true;
        }

        if (Input.GetKeyDown("space") && grounded)
        {
            rb.velocity = Vector2.up * jumpSpeed;
        }

        if (Input.GetKey("left"))
        {
            transform.Translate(speed * Vector2.left * Time.deltaTime);
            faceRight = false;
        }

        if (Input.GetKey("up") && onLadder)
        {
            transform.Translate(speed * Vector2.up * Time.deltaTime);
        }

        if (Input.GetKey("down") && onLadder)
        {
            if (!grounded)
            {
                transform.Translate(speed * Vector2.down * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Platform")
        {
            GameObject[] stairs = GameObject.FindGameObjectsWithTag("Stairs");
            foreach (var st in stairs)
            {
                st.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Stairs" && Input.GetKey("up") && transform.position.x < other.transform.position.x)
        {
            other.GetComponent<Collider2D>().isTrigger = false;
        }
        else if (other.transform.tag == "Stairs" && Input.GetKey("up") && transform.position.x > other.transform.position.x)
        {
            other.GetComponent<Collider2D>().isTrigger = false;
        }

        if (other.gameObject.tag == "Ladder")
        {
            rb.isKinematic = true;
            onLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            rb.isKinematic = false;
            onLadder = false;
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
 
}
