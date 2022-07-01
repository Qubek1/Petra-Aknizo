using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float speed = 5f;
    public float speedJump = 4f;
    public float jumpPower = 5f;
    public float jumpHeight = 0.5f;
    public float jetpackPower = 7f;

    public float rollAcc;
    public float rollSLow;
    float rollSpeed;
    public float rollingAcc;
    public float diameter;

    public LayerMask Ground;
    public Rigidbody2D RB;
    public Transform GroundCheck;

    public Transform GraphicHolder;
    public GameObject jetpackGO;
    public GameObject flameGO;
    public Transform flamePos;
    public GameObject radarGO;
    public GameObject parachuteGO;

    float GravityScale;
    public float parachuteGS = 1f;
    public float parachuteForce = 0.2f;

    bool grounded = false;
    int lastVertical = 0;
    int parachuteState = 0;

    public bool legs = false;
    public bool arms = false;
    public bool jetpack = false;
    public bool radar = false;
    public bool parachute = false;

    public Animator Anim;

    public GameObject Hitbox1;
    public GameObject Hitbox2;

    public GameObject radarMask;

    Transform m_currMovingPlatform;
    bool onPlatform;
    Vector3 platformSpeed;
    public bool GetPowerState(string name)
    {
        switch (name)
        {
            case "Legs_Monitor":
                {
                    return legs;
                }
            case "Arms_Monitor":
                {
                    return arms;
                }
            case "Jetpack_Monitor":
                {
                    return jetpack;
                }
            case "Radar_Monitor":
                {
                    return radar;
                }
            case "Parachute_Monitor":
                {
                    return parachute;
                }

        }
        return false;
    }

    void Awake()
    {
        GravityScale = RB.gravityScale;
        transform.parent = null;
    }

    void Update()
    {
        Anim.SetBool("rolling", !legs);

        if (legs && Time.timeScale!=0)
        {
            Move();
            Jump();
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.33f, Ground);
        Anim.SetBool("grounded", grounded);
        if (!legs)
        {
            Roll();
        }
    }

    void Move()
    {
        int kier = (int)Input.GetAxisRaw("Horizontal");
        if (kier!=0)
        {
            transform.parent = null; // Dla poruszających się platform
            RB.isKinematic = false;
        }
        if(grounded)
            RB.velocity = new Vector2(kier * speed, RB.velocity.y);
        else
            RB.velocity = new Vector2(kier * speedJump, RB.velocity.y);

        if (kier < 0)
            GraphicHolder.localScale = new Vector3(-1, 1, 1);
        if (kier > 0)
            GraphicHolder.localScale = new Vector3(1, 1, 1);
        Anim.SetBool("run", (RB.velocity.x != 0));
    }

    void Roll()
    {
        if (RB.velocity.y < 0 && !grounded && parachute && (int)Input.GetAxisRaw("Parachute") == 1 && parachuteState == 0)
        {
            parachute = false;
            parachuteState = 1;
            GraphicHolder.rotation = new Quaternion(0, 0, 0, 0);
            RB.velocity = new Vector2(0, RB.velocity.y * parachuteForce);
            RB.gravityScale = parachuteGS;
            parachuteGO.SetActive(true);
            parachuteGO.GetComponent<Animator>().SetTrigger("start");
        }
        if(grounded || (int)Input.GetAxisRaw("Parachute") == 0 && parachuteState == 1)
        {
            parachuteState = 0;
            RB.gravityScale = GravityScale;
            parachuteGO.SetActive(false);
        }

        int kier = (int)Input.GetAxisRaw("Horizontal");
        if (kier!=0)
        {
            transform.parent = null; // Dla poruszających się platform
            RB.isKinematic = false;
            //Debug.Log(0);
        }
        if (parachuteState == 0)
        {
            if (!grounded)
            {
                GraphicHolder.Rotate(0, 0, rollSpeed * Time.fixedDeltaTime);
                rollSpeed += rollingAcc * -kier;
                rollSpeed *= 0.96f;
            }
            else
            {
                GraphicHolder.Rotate(0, 0, -RB.velocity.x / Mathf.PI / diameter * Time.fixedDeltaTime * 360f);
                rollSpeed = -RB.velocity.x / Mathf.PI / diameter * 360f;
                if (kier == 0)
                {
                    RB.velocity = new Vector2((RB.velocity.x) * speed * rollSLow / (speed * rollSLow + rollAcc), RB.velocity.y);
                }
                else
                {
                    RB.velocity = new Vector2((RB.velocity.x + rollAcc * kier) * speed / (speed + rollAcc), RB.velocity.y);
                }
            }

            if(jetpack && (int)Input.GetAxisRaw("Vertical") == 1)
            {
                jetpack = false;
                jetpackGO.SetActive(false);
                Instantiate(flameGO, flamePos.position, GraphicHolder.rotation);
                Vector2 F = GraphicHolder.up * jetpackPower;
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.AddForce(F, ForceMode2D.Impulse);
            }
        }
        else
        {
            RB.velocity = new Vector2(kier * speedJump, RB.velocity.y);
            if (kier != 0)
            {
                transform.parent = null; // Dla poruszających się platform
                RB.isKinematic = false;
            }
            if (kier > 0)
                GraphicHolder.localScale = new Vector3(1, 1, 1);
            if (kier < 0)
                GraphicHolder.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Jump()
    {
        if (Input.GetAxisRaw("Vertical")==0 && grounded && onPlatform)
        {
            //transform.parent = null; //Dla poruszającyh sięplatform
            //RB.isKinematic = false;
            RB.velocity = new Vector2(RB.velocity.x, platformSpeed.y);
        }
        if ((int)Input.GetAxisRaw("Vertical") == 1 && lastVertical < 1 && parachuteState == 0)
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
                RB.AddForce(new Vector2(0, jetpackPower), ForceMode2D.Impulse);
                jetpack = false;
                jetpackGO.SetActive(false);
                Instantiate(flameGO, flamePos.position, flamePos.rotation);
            }
        }
        if((int)Input.GetAxisRaw("Vertical") < 1 && lastVertical == 1 && parachuteState == 0)
        {
            //----------------------------------- nie chcemy dalej skakać
            if(RB.velocity.y > 0)
                RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * jumpHeight);
        }

        //--------------------------------------spadochron
        if ((int)Input.GetAxisRaw("Parachute") == 1 && parachute && parachuteState == 0 && RB.velocity.y < 0 && grounded == false)
        {
            parachute = false;
            parachuteState = 1;
            RB.gravityScale = parachuteGS;
            RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * parachuteForce);
            parachuteGO.SetActive(true);
            parachuteGO.GetComponent<Animator>().SetTrigger("start");
        }
        if ((int)Input.GetAxisRaw("Parachute") == 0 || RB.velocity.y >= 0 || grounded)
        {
            parachuteState = 0;
            RB.gravityScale = GravityScale;
            parachuteGO.SetActive(false);
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
        if (collider.tag == "Jetpack" && !jetpack)
        {
            jetpack = true;
            collider.GetComponentInParent<PowerUp>().PickUp();
            jetpackGO.SetActive(true);
        }
        if (collider.tag == "Arms" && !arms && legs)
        {
            arms = true;
            Anim.SetTrigger("arms");
            collider.GetComponentInParent<PowerUp>().PickUp();
        }
        if (collider.tag == "Radar" && !radar)
        {
            radar = true;
            collider.GetComponentInParent<PowerUp>().PickUp();
            radarGO.SetActive(true);
            radarMask.SetActive(true);
        }
        if (collider.tag == "Parachute" && !parachute)
        {
            parachute = true;
            collider.GetComponentInParent<PowerUp>().PickUp();
        }
        if (collider.tag == "Legs" && !legs)
        {
            Hitbox2.SetActive(true);
            Hitbox1.SetActive(false);
            legs = true;
            Anim.SetTrigger("no_arms");
            collider.GetComponentInParent<PowerUp>().PickUp();
            GraphicHolder.rotation = new Quaternion(0,0,0,0);
            //GraphicHolder.position += new Vector3(0, 0.13f, 0);
        }
        if (collider.tag == "Magnes")
        {
            //if(legs)
                //GraphicHolder.position -= new Vector3(0, 0.13f, 0);
            legs = false;
            arms = false;
            radar = false;
            jetpack = false;
            parachute = false;
            GraphicHolder.localScale = new Vector3(1, 1, 1);
            radarGO.SetActive(false);
            jetpackGO.SetActive(false);
            parachuteGO.SetActive(false);
            parachuteState = 0;
            RB.gravityScale = GravityScale;
            Hitbox2.SetActive(false);
            Hitbox1.SetActive(true);
            radarMask.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log(1);
        if (coll.gameObject.tag == "MovingPlatform")
        {
            m_currMovingPlatform = coll.gameObject.transform;
            platformSpeed = coll.gameObject.GetComponent<Rigidbody2D>().velocity;
            onPlatform = true;
            transform.SetParent(m_currMovingPlatform);
            //RB.isKinematic = true;
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "MovingPlatform")
        {
            m_currMovingPlatform = coll.gameObject.transform;
            platformSpeed = coll.gameObject.GetComponent<Rigidbody2D>().velocity;
            transform.SetParent(m_currMovingPlatform);
            this.gameObject.transform.position += new Vector3(platformSpeed.x, 0, 0) * Time.fixedDeltaTime;
            //RB.velocity = new Vector2(RB.velocity.x, platformSpeed.y);
            //RB.isKinematic = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "MovingPlatform")
        {
            //Debug.Log("no");
            m_currMovingPlatform = null;
            RB.isKinematic = false;
            onPlatform = false;
        }
    }

    public void DestroyArms()
    {
        arms = false;
        Anim.SetTrigger("no_arms");
    }
}
