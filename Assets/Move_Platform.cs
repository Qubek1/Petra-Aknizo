using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Platform : MonoBehaviour
{
    public Transform Player;
    public float Speed;
    //public Vector3 direction;
    GameObject Point1, Point2, Plat;
    bool plat_state; //1 - move towards Point2; 0 - move towards Point1
    Rigidbody2D rb;
    void Start()
    {
        Point1 = this.gameObject.transform.Find("Start").gameObject;
        Point2 = this.gameObject.transform.Find("End").gameObject;
        Plat = this.gameObject.transform.Find("Platform").gameObject;
        Plat.transform.position = Point1.transform.position;
        rb = Plat.GetComponent<Rigidbody2D>();
        plat_state = true;
    }

    void FixedUpdate()
    {
        if (plat_state)
        {
            Vector3 now = Plat.transform.position;
            rb.velocity = (Point2.transform.position-Point1.transform.position).normalized*Speed;//(Plat.transform.position + Vector3.ClampMagnitude(Point2.transform.position - Plat.transform.position,1) * Speed * Time.deltaTime);
            //direction = rb.velocity;
            //Debug.Log(Plat.transform.position-now);
            if (Vector2.Distance(Point2.transform.position,Plat.transform.position)<=0.1f)
            {
                plat_state = false;
            }
        }
        else
        {
            rb.velocity = -(Point2.transform.position - Point1.transform.position).normalized * Speed;//.MovePosition(Plat.transform.position + Vector3.ClampMagnitude(Point1.transform.position - Plat.transform.position,1) * Speed * Time.deltaTime);
            //direction = rb.velocity;
            //Debug.Log(Plat.transform.position);
            if (Vector2.Distance(Point1.transform.position,Plat.transform.position)<=0.1f)
            {
                plat_state = true;
            }
        }
        //Player.position += new Vector3(rb.velocity.x,0,0)/Speed * Time.fixedDeltaTime;
        //Debug.Log(new Vector3(rb.velocity.x, 0, 0) / Speed * Time.fixedDeltaTime);
    }
}
