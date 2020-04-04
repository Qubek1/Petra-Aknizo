using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AuM : MonoBehaviour
{
    // Start is called before the first frame update
    public static AuM instance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }

    }
}
