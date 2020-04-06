using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sercret : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.GetComponentInParent<player_controller>().arms = true;
            col.GetComponentInParent<player_controller>().Anim.SetTrigger("DAB");
            Destroy(gameObject);
        }
    }
}
