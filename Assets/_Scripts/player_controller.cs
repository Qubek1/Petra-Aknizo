using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float speed = 5f;
    public float speedJump = 4f;
    public float jumpPower = 5f;
    public float jumpHeight = 0.5f;

    public float rollAcc;
    public float rollSLow;
    public float diameter;

    public LayerMask Ground;
    public Rigidbody2D RB;
    public Transform GroundCheck;

    public GameObject jetpackGO;
    public GameObject flameGO;
    public Transform flamePos;

    public bool grounded = false;
    int lastVertical = 0;

    public bool legs = false;
    public bool arms = false;
    public bool jetpack = false;
    public bool radar = false;
    public bool parachute = false;

    public Animator Anim;

    void Update()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.05f, Ground);

        Anim.SetBool("rolling", !legs);

        if (legs)
        {
            Move();
            Jump();
        }
    }

    void FixedUpdate()
    {
        if(!legs)
        {
            Roll();
        }
    }

    void Move()
    {
        int kier = (int)Input.GetAxisRaw("Horizontal");
        if(grounded)
            RB.velocity = new Vector2(kier * speed, RB.velocity.y);
        else
            RB.velocity = new Vector2(kier * speedJump, RB.velocity.y);

        if (kier < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (kier > 0)
            transform.localScale = new Vector3(1, 1, 1);
        Anim.SetBool("run", (RB.velocity.x != 0));
    }

    void Roll()
    {
        transform.Rotate(0, 0, -RB.velocity.x / Mathf.PI / diameter * Time.fixedDeltaTime * 360f);
        int kier = (int)Input.GetAxisRaw("Horizontal");
        if (kier == 0)
        {
            RB.velocity = new Vector2((RB.velocity.x) * speed * rollSLow / (speed * rollSLow + rollAcc), RB.velocity.y);
        }
        else
        {
            RB.velocity = new Vector2((RB.velocity.x + rollAcc * kier) * speed / (speed + rollAcc), RB.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetAxisRaw("Vertical") == 1 && lastVertical < 1)
        {
            //----------------------------------- probujemy skoczyc
            if (grounded)
            {
                //------------------------------- skok
                RB.velocity = new Vector2(RB.velocity.x, 0f);
                RB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                Anim.SetTrigger("jump");
            }
            else if (jetpack)
            {
                //------------------------------- podwojny skok
                RB.velocity = new Vector2(RB.velocity.x, 0f);
                RB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                jetpack = false;
                jetpackGO.SetActive(false);
                Instantiate(flameGO, flamePos.position, new Quaternion(0,0,0,0));
            }
        }
        if(Input.GetAxisRaw("Vertical") < 1 && lastVertical == 1)
        {
            //----------------------------------- nie chcemy dalej skakać
            if(RB.velocity.y > 0)
                RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * jumpHeight);
        }
        lastVertical = (int)Input.GetAxisRaw("Vertical");
        if (RB.velocity.y < 0)
            Anim.SetInteger("YV", -1);
        else if (RB.velocity.y > 0)
            Anim.SetInteger("YV", 1);
        else
            Anim.SetInteger("YV", 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Jetpack" && !jetpack && legs)
        {
            jetpack = true;
            jetpackGO.SetActive(true);
            collider.GetComponentInParent<PowerUp>().PickUp();
        }
        if (collider.tag == "Arms" && !arms && legs)
        {
            arms = true;
            Anim.SetTrigger("arms");
            collider.GetComponentInParent<PowerUp>().PickUp();
        }
        if (collider.tag == "Legs" && !legs)
        {
            legs = true;
            Anim.SetTrigger("no_arms");
            collider.GetComponentInParent<PowerUp>().PickUp();
            transform.rotation = new Quaternion(0,0,0,0);
            transform.position += new Vector3(0, 0.12f, 0);
        }
    }
}
