using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    /*string curr,nex;
    int nex_num;
    void Start()
    {
        curr = SceneManager.GetActiveScene().name;
        int.TryParse(curr.Substring(2, 1), out nex_num);
        nex_num += 1;
        nex = curr.Substring(0, 2) + nex_num;
    }*/
    public string nex;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag!="Player")
        {
            return;
        }
        /*if (SceneManager.GetSceneByName(nex).IsValid())
        {*/
            SceneManager.LoadScene(nex);
        /*}
        else
        {
            SceneManager.LoadScene("Start");
        }*/
    }
}
