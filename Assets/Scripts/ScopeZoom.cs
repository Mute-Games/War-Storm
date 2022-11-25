using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeZoom : MonoBehaviour
{
    bool IsZooming;

    public GameObject ScopeCam;
    public GameObject NormalCam;

    public Camera cam;

    bool latestart;

    // Start is called before the first frame update
    void Start()
    {
        latestart = true;
        

        IsZooming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (latestart)
        {
            cam = GameObject.FindGameObjectWithTag("ScopeCam").GetComponent<Camera>();
            latestart = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (IsZooming)
            {
                IsZooming = false;
                NormalCam.SetActive(true);
                ScopeCam.SetActive(false);
            }
            else
            {
                IsZooming = true;
                NormalCam.SetActive(false);
                ScopeCam.SetActive(true);
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            if (cam.fieldOfView == 10)
            {
                cam.fieldOfView = 60;
            }
            else if (cam.fieldOfView == 60)
            {
                cam.fieldOfView = 45;
            }
            else if (cam.fieldOfView == 45)
            {
                cam.fieldOfView = 25;
            }
            else if (cam.fieldOfView == 25)
            {
                cam.fieldOfView = 10;
            }
        }
    }
}
