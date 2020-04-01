using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private bool legs;
    [SerializeField]
    private bool hands;
    [SerializeField]
    private bool wings;
    [SerializeField]
    private bool jetpack;
    [SerializeField]
    private bool radar;
    public GameObject legsInv;
    public GameObject handsInv;
    public GameObject wingsInv;
    public GameObject jetpackInv;
    public GameObject radarInv;




    void Start()
    {
        legs = false;
        hands = false;
        wings = false;
        jetpack = false;
        radar = false;
}

    void Update()
    {
        if (!legs)
        {
            this.GetComponent<MovementController>().CanJump(false);
            legsInv.SetActive(false);
        }
        else
        {
            this.GetComponent<MovementController>().CanJump(true);
            legsInv.SetActive(true);
        }
        if (!hands)
        {
            this.GetComponent<MovementController>().CanDrag(false);
            handsInv.SetActive(false);
        }
        else
        {
            this.GetComponent<MovementController>().CanDrag(true);
            handsInv.SetActive(true);
        }
        if (!wings)
        {
            //this.GetComponent<MovementController>().CanJump(false);
            wingsInv.SetActive(false);
        }
        else
        {
            //this.GetComponent<MovementController>().CanJump(true);
            wingsInv.SetActive(true);
        }
        wingsInv.SetActive(wings);
        if (!jetpack)
        {
            //this.GetComponent<MovementController>().CanJump(false);
            jetpackInv.SetActive(false);
        }
        else
        {
            //this.GetComponent<MovementController>().CanJump(true);
            jetpackInv.SetActive(true);
        }
        if (!!!radar == true)
        {
            //this.GetComponent<MovementController>().CanJump(false);
            radarInv.SetActive(false);
        }
        else
        {
            //this.GetComponent<MovementController>().CanJump(true);
            radarInv.SetActive(true);
        }
    }
    public void Legs(bool x)
    {
        legs = x;
    }
    public void Hands(bool x)
    {
        hands = x;
    }
    public void Wings(bool x)
    {
        wings = x;
    }
    public void Jetpack(bool x)
    {
        jetpack = x;
    }
    public void Radar(bool x)
    {
        radar = x;
    }
}
