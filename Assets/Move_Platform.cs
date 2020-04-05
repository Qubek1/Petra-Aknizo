using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Platform : MonoBehaviour
{
    public float Speed;
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
            rb.MovePosition(Plat.transform.position + Vector3.ClampMagnitude(Point2.transform.position - Plat.transform.position,1) * Speed * Time.deltaTime);
            //Debug.Log(Plat.transform.position);
            if (Vector2.Distance(Point2.transform.position,Plat.transform.position)<=0.1f)
            {
                plat_state = false;
            }
        }
        else
        {
            rb.MovePosition(Plat.transform.position + Vector3.ClampMagnitude(Point1.transform.position - Plat.transform.position,1) * Speed * Time.deltaTime);
            //Debug.Log(Plat.transform.position);
            if (Vector2.Distance(Point1.transform.position,Plat.transform.position)<=0.1f)
            {
                plat_state = true;
            }
        }

    }
}
