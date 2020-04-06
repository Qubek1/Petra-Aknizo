using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    public Transform[] Object;
    public Transform Player;

    void Update()
    {
        for(int i=0; i<Object.Length; i++)
        {
            Object[i].localPosition = new Vector3(-Player.position.x * Object[i].position.z / 10,
                -Player.position.y * Object[i].position.z / 10,
                Object[i].position.z);
        }
    }
}
