using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObjectController : MonoBehaviour
{
    public GameObject player;
    private Camera mainCamera;   
    public Camera portalCamera;

    public GameObject portalAxis;
    public GameObject otherPortal;
    private Vector3 angleDiff;

    private int collidingObjectCount = 0;

    void Start()
    {
        mainCamera = Camera.main;
        //portalPassManager = this.transform.parent.GetComponent<PortalPassManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        collidingObjectCount++;
        if (other.CompareTag("Player"))
        {
            SetPassingLayer(other);
        }
        if (other.CompareTag("PassableObject"))
        {
            SetPassingLayer(other.transform.parent.GetComponent<Collider>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        collidingObjectCount--;
        if (other.CompareTag("Player"))
        {
            SetDefaultLayer(other);
        }
        if (other.CompareTag("PassableObject"))
        {
            SetDefaultLayer(other.transform.parent.GetComponent<Collider>());
        }
    }
    void SetPassingLayer(Collider collision)
    {
        collision.gameObject.layer = LayerMask.NameToLayer("PassingObject");
    }
    void SetDefaultLayer(Collider collision)
    {
        collision.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    // Update is called once per frame
    void Update()
    {
        angleDiff = this.transform.up * 180.0f;
        portalCamera.transform.position = this.transform.position+ (-(Quaternion.Euler(angleDiff+this.transform.rotation.eulerAngles - otherPortal.transform.rotation.eulerAngles) *
            (otherPortal.transform.position - mainCamera.transform.position)));
        portalCamera.transform.rotation = Quaternion.Euler(angleDiff + (mainCamera.transform.rotation.eulerAngles+ (this.transform.rotation.eulerAngles - otherPortal.transform.rotation.eulerAngles)));
    }
}
