using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveForce = 60;
    public float jumpForce = 100;
    public float maxSpeed = 10;
    public float maxVSpeed = 10;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public bool grounded { get; private set; }
    private Rigidbody2D rb2d;

    public bool facingLeft {
        get {
            return transform.localScale.x < 0;
        }
        set {
            float x = (value) ? Mathf.Abs(transform.localScale.x) * -1 : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }
    }

    public int facingSign { get { return (facingLeft) ? -1 : 1; } }

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate() {
        float h_axis = Input.GetAxis("Horizontal");
        grounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);

        if(rb2d.velocity.x < maxSpeed && h_axis > 0 || rb2d.velocity.x > -maxSpeed && h_axis < 0)
            rb2d.AddForce(new Vector2(h_axis * moveForce, 0));

        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed), Mathf.Min(rb2d.velocity.y, maxVSpeed));

        if((facingLeft && h_axis > 0.01f) || (!facingLeft && h_axis < -0.01f))
            Flip();

        if(grounded && Input.GetKeyDown(KeyCode.Space))
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    protected void Flip() {
        facingLeft = !facingLeft;
    }
}
