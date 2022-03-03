using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHoldManager : MonoBehaviour
{
    GameObject player;
    public Camera mainCamera;
    public Collider holdingObject;
    public bool isHolding = false;
    public bool isInteracting = false;
    public float rayReach = 8.0f;
    public float holdingReach = 2.0f;
    public float holdingSmoothness = 10.0f;
    public float throwingForce = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHolding)
        {
            if (!isInteracting)
                holdingObject.transform.position =
                    Vector3.Lerp(holdingObject.transform.position,
                    (mainCamera.transform.position + mainCamera.transform.forward * holdingReach),
                    Time.deltaTime * holdingSmoothness);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
            {
                Hold();
                //레이져 쏘고 들수있는 물건이 맞으면 들음
                
            }
            else//isHolding
            {
                UnHold();
            }
        }

    }
    void Hold()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayReach))
        {
            if (hit.collider.CompareTag("HoldableObject"))
            {
                holdingObject = hit.collider;
                holdingObject.GetComponent<Rigidbody>().isKinematic = true;
                isHolding = true;
                holdingObject.gameObject.layer = LayerMask.NameToLayer("HoldingObject");
            }
        }

    }
    void UnHold()
    {
        isHolding = false;
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        /*던지기 기능 추가 코드*/
        holdingObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwingForce);
        /*던지기 기능 추가 코드*/
        holdingObject.gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
