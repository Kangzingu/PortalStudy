using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingObjectController : MonoBehaviour
{
    public GameObject duplicateObject;
    public GameObject collisionMeshFilter;
    public Material material;
    Mesh collisionMesh;
    // Start is called before the first frame update
    void Start()
    {
        collisionMesh = collisionMeshFilter.GetComponent<MeshFilter>().mesh;
        //collisionMesh = collision.collider.GetComponent<Mesh>();
        for (int i = 0; i < collisionMesh.triangles.Length; i++)
        {
            Debug.Log(((int)collisionMesh.triangles.GetValue(i)));
            Debug.Log(collisionMesh.vertices.GetValue((int)collisionMesh.triangles.GetValue(i)));
            //collisionMesh.vertices.GetValue(collisionMesh.triangles.GetValue(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + this.GetComponent<Rigidbody>().velocity,Color.red);
        //Graphics.DrawMesh(collisionMesh, collisionMeshFilter.transform.position+Vector3.up, Quaternion.identity,material,0);
        //duplicateObject.transform.position = collisionMeshFilter.transform.position+Vector3.up;
        //duplicateObject.transform.rotation = collisionMeshFilter.transform.rotation;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.contactCount);
    }
}
