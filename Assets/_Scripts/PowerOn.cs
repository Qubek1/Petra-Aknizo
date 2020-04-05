using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerOn : MonoBehaviour
{
    public GameObject On, Off;
    public player_controller pc;
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    void Start()
    {
        On = GetChildWithName(gameObject, "On");
        Off = GetChildWithName(gameObject, "Off");
    }
    public void SwitchOnff(bool onff)
    {
        On.SetActive(onff);
        Off.SetActive(!onff);
    }
    void Update()
    {
        bool x = pc.GetPowerState(gameObject.name);
        SwitchOnff(x);
        if (gameObject.name == "Jetpack_Monitor")
        {
            Debug.Log(gameObject.name + " " + x);
        }
    }
}
