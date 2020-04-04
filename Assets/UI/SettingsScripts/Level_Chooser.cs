using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Chooser : MonoBehaviour
{
    // Start is called before the first frame update
    Text lvl;
    //Button btn;
    public Scene_Man_Script sms;

    void Start()
    {
        lvl = gameObject.GetComponent<Text>();
        //btn = gameObject.GetComponent<Button>();
        lvl.text = gameObject.name;
        //btn.onClick.AddListener();
    }

    public void InvLoad()
    {
        sms.Load(gameObject.name);
    }


}
