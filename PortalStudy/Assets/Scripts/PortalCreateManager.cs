using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCreateManager : MonoBehaviour
{
    Camera mainCamera;
    public GameObject leftPortal;
    public GameObject rightPortal;
    public GameObject leftPortalAttachedObject;
    public GameObject rightPortalAttachedObject;
    public GameObject leftPortalBarrier;
    public GameObject rightPortalBarrier;

    public ObjectHoldManager objectHoldManager;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MakePortal(leftPortal, 0);
        else if (Input.GetMouseButtonDown(1))
            MakePortal(rightPortal, 1);
    }
    void MakePortal(GameObject portal, int portalType)
    {
        //물체를 든 상태에선 포탈생성 불가
        if (!objectHoldManager.isHolding)
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("PortalAttachableObject"))
                {
                    //이전의 포탈 붙었던 물체 카메라 레이어 해제
                    //새로 붙은 물체 카메라 레이어 설정
                    if (portalType == 0)
                    {
                        if (leftPortalAttachedObject != null)
                        {
                            leftPortalAttachedObject.layer = LayerMask.NameToLayer("Default");
                        }
                        hit.transform.gameObject.layer = LayerMask.NameToLayer("LeftPortalIgnoreObject");
                        leftPortalAttachedObject = hit.transform.gameObject;
                    }
                    else
                    {
                        if (rightPortalAttachedObject != null)
                        {
                            rightPortalAttachedObject.layer = LayerMask.NameToLayer("Default");
                        }
                        hit.transform.gameObject.layer = LayerMask.NameToLayer("RightPortalIgnoreObject");
                        rightPortalAttachedObject = hit.transform.gameObject;
                    }
                    //포탈이 한 오브젝트에 같이 붙은 경우
                    if (rightPortalAttachedObject == leftPortalAttachedObject)
                    {
                        hit.transform.gameObject.layer = LayerMask.NameToLayer("BothPortalIgnoreObject");
                    }
                    //위치에 포탈 생성
                    Quaternion hitObjectRotationQuat;
                    hitObjectRotationQuat = Quaternion.LookRotation(new Vector3(hit.normal.x, hit.normal.y, hit.normal.z));//Quaternion.Euler(hitObjectRotation);
                    portal.transform.position = hit.point;
                    portal.transform.rotation = Quaternion.Euler(hitObjectRotationQuat.eulerAngles);

                    //포탈 벽 세우기
                    if (portalType == 0)
                    {
                        leftPortalBarrier.transform.position = hit.point;
                        leftPortalBarrier.transform.rotation = Quaternion.Euler(hitObjectRotationQuat.eulerAngles);
                    }
                    else
                    {
                        rightPortalBarrier.transform.position = hit.point;
                        rightPortalBarrier.transform.rotation = Quaternion.Euler(hitObjectRotationQuat.eulerAngles);
                    }
                }
            }
        }
    }
}
