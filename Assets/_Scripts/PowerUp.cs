using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject Graphic;
    public float ReTime = 2f;
    void Start()
    {
        Graphic = this.gameObject.transform.GetChild(0).gameObject;
    }
    public void PickUp()
    {
        Graphic.SetActive(false);
        //zmiana grafiki (lub jej usunięcie)
        //particle
        StartCoroutine(Reactivation(ReTime));
    }
    IEnumerator Reactivation(float rt)
    {
        yield return new WaitForSeconds(rt);
        Graphic.SetActive(true);
    }
}
