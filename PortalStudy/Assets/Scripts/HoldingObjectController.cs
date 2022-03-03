using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingObjectController : MonoBehaviour
{

    public ObjectHoldManager ohManager;
    //public Material material;
    //Mesh collisionMesh;
    //// Start is called before the first frame update
    void Start()
    {
        //    collisionMesh = collisionMeshFilter.GetComponent<MeshFilter>().mesh;
        //    //collisionMesh = collision.collider.GetComponent<Mesh>();
        //    for (int i = 0; i < collisionMesh.triangles.Length; i++)
        //    {
        //        Debug.Log(((int)collisionMesh.triangles.GetValue(i)));
        //        Debug.Log(collisionMesh.vertices.GetValue((int)collisionMesh.triangles.GetValue(i)));
        //        //collisionMesh.vertices.GetValue(collisionMesh.triangles.GetValue(i));
        //    }
        //
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = ohManager.mainCamera.transform.position + ohManager.mainCamera.transform.forward * ohManager.holdingReach;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other != ohManager.holdingObject)
        {
            if (ohManager.isHolding)
            {
                ohManager.isInteracting = true;
                ohManager.holdingObject.GetComponent<Rigidbody>().isKinematic = false;
                ohManager.holdingObject.GetComponent<SpringJoint>().connectedBody = this.GetComponent<Rigidbody>();
                ohManager.holdingObject.GetComponent<SpringJoint>().anchor = Vector3.zero;
                ohManager.holdingObject.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
                ohManager.holdingObject.GetComponent<SpringJoint>().connectedAnchor = Vector3.zero;
                ohManager.holdingObject.GetComponent<SpringJoint>().spring = 100;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other != ohManager.holdingObject)
        {
            if (ohManager.isInteracting)
            {
                ohManager.isInteracting = false;
                ohManager.holdingObject.GetComponent<Rigidbody>().isKinematic = true;
                ohManager.holdingObject.GetComponent<SpringJoint>().connectedBody = null;
                ohManager.holdingObject.GetComponent<SpringJoint>().anchor = Vector3.zero;
                ohManager.holdingObject.GetComponent<SpringJoint>().spring = 0;

            }
        }
    }
}
