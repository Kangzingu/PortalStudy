using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPassManager : MonoBehaviour
{
    public GameObject player;
    public GameObject otherPortal;
    public bool isPassing = false;
    private bool isLeftPortal = false;

    private Vector3 angleDiff;
    private Vector3 posDiff;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerExit(Collider other)
    {
        if (isPassing == true)
        {
            if (other.CompareTag("Player"))
            {
            //    if (isLeftPortal)//left
                    //player.GetComponent<PortalCreateManager>().leftPortalAttachedObject.GetComponent<Collider>().isTrigger = false;
            //    else
                    //player.GetComponent<PortalCreateManager>().rightPortalAttachedObject.GetComponent<Collider>().isTrigger = false;
            }
            else if (other.CompareTag("PassableObject"))
            {
            //    if (isLeftPortal)//left
            //        player.GetComponent<PortalCreateManager>().leftPortalAttachedObject.GetComponent<Collider>().isTrigger = false;
            //    else
            //        player.GetComponent<PortalCreateManager>().rightPortalAttachedObject.GetComponent<Collider>().isTrigger = false;
            }
            isPassing = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isPassing)//새로 들어온넘
            {
                otherPortal.GetComponent<PortalPassManager>().isPassing = true;
                angleDiff = otherPortal.transform.rotation.eulerAngles - this.transform.rotation.eulerAngles+(otherPortal.transform.up*180);
                posDiff = this.transform.position - other.transform.position;
                other.GetComponent<PlayerCameraController>().x += angleDiff.y;
                other.transform.position = otherPortal.transform.position - Quaternion.Euler(angleDiff) * posDiff;
                other.GetComponent<Rigidbody>().velocity = Quaternion.Euler(angleDiff) * other.GetComponent<Rigidbody>().velocity;
                //isPassing = true;
            }
        }
        if (other.CompareTag("PassableObject"))
        {
            if (!isPassing)
            {
                other = other.transform.parent.GetComponent<Collider>();
                otherPortal.GetComponent<PortalPassManager>().isPassing = true;
                angleDiff = otherPortal.transform.rotation.eulerAngles - this.transform.rotation.eulerAngles + (otherPortal.transform.up * 180);
                posDiff = Quaternion.Euler(angleDiff) * (other.transform.position - this.transform.position);
                other.transform.rotation = Quaternion.Euler(angleDiff) * other.transform.rotation;
                other.transform.position = otherPortal.transform.position + posDiff;
                other.GetComponent<Rigidbody>().velocity = Quaternion.Euler(angleDiff) * other.GetComponent<Rigidbody>().velocity;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
