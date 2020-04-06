using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamMan : MonoBehaviour
{
    bool Activated = false;
    public GameObject UC, PBC;
    CinemachineVirtualCamera UCCVC, PBCCVC;
    public Escape_Listener EL;
    void Start()
    {
        UCCVC = UC.GetComponent<CinemachineVirtualCamera>();
        PBCCVC = PBC.GetComponent<CinemachineVirtualCamera>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("elo Mordo");
            Activated = !Activated;
        }
        if (!Activated)
        {
            PBCCVC.m_Priority = 10;
            UCCVC.m_Priority = 0;
            PBC.SetActive(true);
            UC.SetActive(false);
            UC.transform.position = PBC.transform.position;
            if(!EL.paused)
                Time.timeScale = 1;
        }
        else
        {

            PBCCVC.m_Priority = 0;
            UCCVC.m_Priority = 10;
            PBC.SetActive(false);
            UC.SetActive(true);
            Time.timeScale = 0;
        }
    }

}