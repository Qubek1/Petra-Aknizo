using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool canDrag;
    public bool isGrounded;
    public float speed = 10.0f;
    public float jumpForce;
    private Rigidbody2D rb;
    public Vector3 movement;
    public float startPos1;
    public float startPos2;
    public bool ok;
    public bool collider;
    private Collider2D cos;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ok = true;
            startPos1 = transform.position.x;
            startPos2 = cos.transform.parent.position.x;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ok = false;
        }
        Jump();
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*speed, rb.velocity.y) ;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    void Drag(Collider2D other)
    {
        
        if (Input.GetKey(KeyCode.Space) && isGrounded && canDrag && ok)
        {
            other.transform.parent.position = new Vector3(startPos2 +transform.position.x - startPos1, other.transform.parent.position.y, other.transform.parent.position.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            isGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Legs")
        {
            this.GetComponent<PowerUpController>().Legs(true);
        }
        if (other.tag == "Hands")
        {
            this.GetComponent<PowerUpController>().Hands(true);
        }
        if (other.tag == "Wings")
        {
            this.GetComponent<PowerUpController>().Wings(true);
        }
        if (other.tag == "Jetpack")
        {
            this.GetComponent<PowerUpController>().Jetpack(true);
        }
        if (other.tag == "Radar")
        {
            this.GetComponent<PowerUpController>().Radar(true);
        }
        if (other.tag == "LegsOff")
        {
            this.GetComponent<PowerUpController>().Legs(false);
        }
        if (other.tag == "HandsOff")
        {
            this.GetComponent<PowerUpController>().Hands(false);
        }
        if (other.tag == "WingsOff")
        {
            this.GetComponent<PowerUpController>().Wings(false);
        }
        if (other.tag == "JetpackOff")
        {
            this.GetComponent<PowerUpController>().Jetpack(false);
        }
        if (other.tag == "RadarOff")
        {
            this.GetComponent<PowerUpController>().Radar(false);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        cos = other;
        collider = true;
        if (other.tag == "DragCollider")
        {
            Drag(other);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        ok = false;
        collider = false;
    }








    public void CanJump(bool x)
    {
        canJump = x;
    }
    public void CanDrag(bool x)
    {
        canDrag = x;
    }
}