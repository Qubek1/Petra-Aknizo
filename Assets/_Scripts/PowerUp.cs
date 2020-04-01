using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject Graphic;

    public void PickUp()
    {
        Graphic.SetActive(false);
        //zmiana grafiki (lub jej usunięcie)
        //particle
    }
}
