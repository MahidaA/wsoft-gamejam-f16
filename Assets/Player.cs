using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int speed = 3;
    public int jumpSpeed = 5;
    Rigidbody2D rb;
    Collider2D coll;
    bool faceRight;
    public bool grounded;
	public bool onLadder;
    int layermask;
    public bool onStairs;

	public float ladderX;

	public bool ignore;

	public void disable(float time){
		ignore=true;
		Debug.Log(time);
		StartCoroutine(waitThenGainControl(time));
	}

	private IEnumerator waitThenGainControl(float time){
		Debug.Log("Start");
		yield return new WaitForSecondsRealtime(time);
		ignore=false;
		Debug.Log("END");
	}

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
		

		RaycastHit2D hit=Physics2D.BoxCast(transform.position+coll.bounds.extents,
								new Vector3(coll.bounds.extents.x*2, 0.1F),
								0, Vector3.down,coll.bounds.extents.y+0.2f);
//		RaycastHit2D hit = Physics2D.Raycast(transform.position+coll.bounds.extents, -Vector2.up,  coll.bounds.extents.y+0.2f);
//		Debug.DrawRay(transform.position+new Vector3(coll.bounds.extents.x, 0), -Vector2.up*(0.2f));

		RaycastHit2D staircheckr = Physics2D.Raycast(transform.position+new Vector3(coll.bounds.extents.x, 0), -Vector2.up + Vector2.right, Mathf.Sqrt(Mathf.Pow(coll.bounds.extents.y, 2) + Mathf.Pow(coll.bounds.extents.x, 2)) + 0.5f, layermask);

		RaycastHit2D staircheckl = Physics2D.Raycast(transform.position+new Vector3(coll.bounds.extents.x, 0), -Vector2.up + -Vector2.right, Mathf.Sqrt(Mathf.Pow(coll.bounds.extents.y, 2) + Mathf.Pow(coll.bounds.extents.x, 2)) + 0.5f, layermask);

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

		if(rb.velocity.y>0){
			grounded=false;
			return;
		}

		grounded=hit;

//        if (!hit)
//        {
//            grounded = false;
//            return;
//        }
//
//		if (hit.transform.tag == "Platform" || hit.transform.tag == "pcol" || hit.transform.tag=="Platform_NonSolid")
//        {
//            grounded = true;
//        }
//        else
//        {
//            grounded = false;
//        }
    }

    // Update is called once per frame
    void Update () {
		if(ignore) return;

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


		RaycastHit2D hit=new RaycastHit2D();
		if(onLadder)
			hit = Physics2D.Raycast(transform.position+new Vector3(coll.bounds.extents.x, 0), -Vector2.up,  0.3f);

		if (Input.GetKey("right") && (!onLadder||hit))
        {
			if(onStairs)
				transform.Translate(speed * Vector2.right * Time.deltaTime);
			rb.velocity=new Vector2(speed, rb.velocity.y);
            faceRight = true;
		}else if (Input.GetKey("left") && (!onLadder||hit))
		{
			if(onStairs)
				transform.Translate(speed * Vector2.left * Time.deltaTime);
			rb.velocity=new Vector2(-speed, rb.velocity.y);
			faceRight = false;
		}else{
			rb.velocity=new Vector2(0, rb.velocity.y);
		}

		if(Input.GetKey("right")&&Input.GetKey("left")){
			rb.velocity=new Vector2(0, rb.velocity.y);
		}

		if (Input.GetKeyDown("space") && (grounded||hit))
        {
            rb.velocity = Vector2.up * jumpSpeed;
			GetComponent<PlayerAnimationController>().jumping=true;
			grounded=false;
        }



        if (Input.GetKey("up") && onLadder)
        {
			
			if(hit.collider==null || hit.collider.tag!="Platform_NonSolid"){
				transform.Translate(speed * Vector2.up * Time.deltaTime);
				transform.position-=new Vector3(transform.position.x-ladderX,0,0);
			}
        }

        if (Input.GetKey("down") && onLadder)
        {
            if (!grounded)
            {
				transform.Translate(speed * Vector2.down * Time.deltaTime);
				transform.position-=new Vector3(transform.position.x-ladderX,0,0);
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

		if(other.GetComponent<Enemy>()!=null){
			GameObject.FindObjectOfType<Game>().gameOver();
		}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            rb.isKinematic = false;
            onLadder = false;
        }

		if (other.gameObject.tag == "Ledge"){
			rb.velocity=new Vector2(0,0);
		}
    }

    void OnTriggerStay2D(Collider2D other)
    {
		GetComponent<PlayerAnimationController>().ledge=false;
        if (other.gameObject.tag == "Ledge")
        {
            if (other.gameObject.transform.position.x >= transform.position.x &&
            Input.GetKey("right") && !grounded)
            {
//                transform.Translate(Vector2.up * 2 * Time.deltaTime);
				rb.velocity=new Vector2(1,4);
				GetComponent<PlayerAnimationController>().ledge=true;
//                transform.Translate(Vector2.right * Time.deltaTime);
            }

            if (other.gameObject.transform.position.x <= transform.position.x &&
            Input.GetKey("left") && !grounded)
            {
				rb.velocity=new Vector2(-1,4);
				GetComponent<PlayerAnimationController>().ledge=true;
//                transform.Translate(Vector2.up * 2 * Time.deltaTime);
//                transform.Translate(Vector2.left * Time.deltaTime);
            }
        }

		if (other.gameObject.tag == "Ladder")
		{
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)){
				rb.isKinematic = true;
				onLadder = true;
				ladderX=other.transform.position.x+other.bounds.extents.x-coll.bounds.extents.x;
			}
		}
    }
 
}
