using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    GameObject parent_tilemap;
    // Start is called before the first frame update
    void Start()
    {
        parent_tilemap = this.transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<player_controller>().arms)
        {
            parent_tilemap.SetActive(false);
        }
    }
}