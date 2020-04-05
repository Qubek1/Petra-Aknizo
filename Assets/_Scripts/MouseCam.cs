using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 40f;
    public BoxCollider2D Confiner;

    void Update()
    {
        Vector3 pos = transform.position;
        float CxMin,CxMax, CyMin, CyMax;
        CxMin = Confiner.offset.x - Confiner.size.x/2 + Camera.main.orthographicSize * Camera.main.aspect;
        CxMax = Confiner.offset.x + Confiner.size.x/2 - Camera.main.orthographicSize * Camera.main.aspect;
        CyMin = Confiner.offset.y - Confiner.size.y/ 2 + Camera.main.orthographicSize;// Confiner.offset.y;
        CyMax = Confiner.offset.y + Confiner.size.y/ 2 - Camera.main.orthographicSize;

        if (Input.GetAxisRaw("Vertical")==1 || 
            Input.mousePosition.y >= Screen.height - panBorderThickness)
        { pos.y += panSpeed * Time.unscaledDeltaTime; }
        if (Input.GetAxisRaw("Vertical")==-1 ||
            Input.mousePosition.y <= panBorderThickness)
        { pos.y -= panSpeed * Time.unscaledDeltaTime; }
        if (Input.GetAxisRaw("Horizontal") == 1 ||
            Input.mousePosition.x >= Screen.width-panBorderThickness)
        { pos.x += panSpeed * Time.unscaledDeltaTime; }
        if (Input.GetAxisRaw("Horizontal") == -1 ||
            Input.mousePosition.x <= panBorderThickness)
        { pos.x -= panSpeed * Time.unscaledDeltaTime; }
        pos.x = Mathf.Max(CxMin, pos.x);
        pos.x = Mathf.Min(CxMax, pos.x);
        pos.y = Mathf.Max(CyMin, pos.y);
        pos.y = Mathf.Min(CyMax, pos.y);
        transform.position = pos;
    }
}
