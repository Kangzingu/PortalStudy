using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject[] portal = new GameObject[2];
    public Camera[] portalCamera = new Camera[2];

    public bool isCustomClippingPlane = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            if (Input.GetMouseButtonDown(0))
                MakePortal(0);
            else if (Input.GetMouseButtonDown(1))
                MakePortal(1);
        }
    }
    void MakePortal(int portalType)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (portalType == 0)
            {
                hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial.SetVector("_LeftPortalCenterPoint", hit.point);
                hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial.SetVector("_LeftPortalNorm", hit.normal);
                //leftPortalAttachedObject = hit.transform.gameObject;
            }
            else
            {
                hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial.SetVector("_RightPortalCenterPoint", hit.point);
                hit.collider.gameObject.GetComponent<Renderer>().sharedMaterial.SetVector("_RightPortalNorm", hit.normal);
            }//쉐이더를 통해 포탈을 보이게 해줌

            //이제 포탈 객체가 하는일은, 카메라의 위치잡기를 위해 옮겨지는것, 콜라이더 역할
            Quaternion hitObjectRotationQuat;
            hitObjectRotationQuat = Quaternion.LookRotation(new Vector3(hit.normal.x, hit.normal.y, hit.normal.z));//Quaternion.Euler(hitObjectRotation);
            portal[portalType].transform.position = hit.point;
            portal[portalType].transform.rotation = Quaternion.Euler(hitObjectRotationQuat.eulerAngles);
            //SetCameraNearClip(portalType, hit);
        }
    }
    private void LateUpdate()
    {
        if (isCustomClippingPlane)
        {
            SetCameraNearClip(0, portal[0]);
            SetCameraNearClip(1, portal[1]);
        }
    }
    void SetCameraNearClip(int portalType, GameObject hit)
    {
        Vector4 clipPlaneWorld = 
            new Vector4(hit.transform.forward.x, hit.transform.forward.y, hit.transform.forward.z,
            Vector3.Dot(hit.transform.position, -hit.transform.forward));
        Vector4 clipPlaneCamera =
            Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera[portalType].worldToCameraMatrix)) * clipPlaneWorld;
        portalCamera[portalType].projectionMatrix = playerCamera.CalculateObliqueMatrix(clipPlaneCamera);
    }
}
