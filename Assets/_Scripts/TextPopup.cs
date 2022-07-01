using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public GameObject popup;
    public AnimationCurve popupSizeAnimation;
    public Collider2D triggerArea;
    public bool armsRequirement;
    public bool parachuteRequirement;
    private bool active = false;
    private float activationTime;

    private void Start()
    {
        popup.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.tag == "Player")
        {
            if (armsRequirement && !collision.attachedRigidbody.gameObject.GetComponent<player_controller>().arms)
                return;
            if (parachuteRequirement && !collision.attachedRigidbody.gameObject.GetComponent<player_controller>().parachute)
                return;
            active = true;
            activationTime = Time.time;
            popup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.tag == "Player")
        {
            active = false;
            popup.SetActive(false);
        }
    }

    private void Update()
    {
        if(active)
        {
            popup.transform.localScale = new Vector3(1, 1, 1) * popupSizeAnimation.Evaluate(Time.time - activationTime);
        }
    }
}
