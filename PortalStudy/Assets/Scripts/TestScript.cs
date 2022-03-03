using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Camera mainCam;
    public GameObject portal1;
    public GameObject portal2;
    public Camera leftPortalCamera;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(mainCam.transform.position, portal1.transform.position, Color.black);
        Debug.DrawLine(leftPortalCamera.transform.position, portal2.transform.position, Color.black);
        //Vector3 intersectionDir=Vector3.Cross(portal1.transform.forward, mainCam.transform.forward);
        //Debug.DrawLine(mainCam.transform.position, mainCam.transform.position + intersectionDir,Color.red);
        //Ray ray = new Ray(mainCam.transform.position + mainCam.transform.forward * mainCam.nearClipPlane, portal1.transform.forward);
        //Debug.DrawLine(mainCam.transform.position + mainCam.transform.forward * mainCam.nearClipPlane, mainCam.transform.position + mainCam.transform.forward * mainCam.nearClipPlane+ portal1.transform.forward, Color.blue);
        //Vector3 intersectionPos;
    }
}
