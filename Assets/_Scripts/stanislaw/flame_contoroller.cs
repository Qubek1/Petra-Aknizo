using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flame_contoroller : MonoBehaviour
{
    float time = -1f;
    private void Start()
    {
        time = Time.time;
    }
    private void Update()
    {
        if(Time.time - time > 0.40f && time != -1)
        {
            Destroy(gameObject);
        }
    }
}
