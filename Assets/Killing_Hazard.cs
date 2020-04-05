using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killing_Hazard : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Player")
        {
            Debug.Log("Hit!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
