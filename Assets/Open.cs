using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    bool OnTrigger = false;
    GameObject parent_tilemap;
    public player_controller pc;
    // Start is called before the first frame update
    void Start()
    {
        parent_tilemap = this.transform.parent.gameObject;
        Debug.Log(parent_tilemap.name);
    }

    private void Update()
    {
        if(OnTrigger && Input.GetAxisRaw("ButtonPress") == 1)
        {
            pc.DestroyArms();
            parent_tilemap.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponentInParent<player_controller>().arms)
        {
            OnTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponentInParent<player_controller>().arms)
        {
            OnTrigger = false;
        }
    }
}