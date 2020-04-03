using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash : MonoBehaviour
{
    public float lifeTime = 0.4f;
    float time = -1f;
    private void Start()
    {
        time = Time.time;
    }
    private void Update()
    {
        if(Time.time - time > lifeTime && time != -1)
        {
            Destroy(gameObject);
        }
    }
}
