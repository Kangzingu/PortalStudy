using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public GameObject cam;
    private Animator playerAnimator;
    public float rotationSensitivity = 2;
    public float lerpSensitivity = 30;
    public float yMinLimit = -89f;
    public float yMaxLimit = 89f;
    public Rigidbody player;
    public GameObject head;
    public float x, y;

    Quaternion initCamRotation;
    Quaternion initHeadRotation;


    // Start is called before the first frame update
    void Start()
    {
        initCamRotation = cam.transform.rotation;
        initHeadRotation = head.transform.rotation;
        playerAnimator = player.GetComponent<Animator>();
        Vector3 angles = cam.transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame

    void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        x += Input.GetAxis("Mouse X") * rotationSensitivity;
        y = ClampAngle(y - Input.GetAxis("Mouse Y") * rotationSensitivity, yMinLimit, yMaxLimit);
        //avatarAnimator.SetFloat("mouseX", Input.GetAxis("Mouse X") * rotationSensitivity);
        // Rotation
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.AngleAxis(x, Vector3.up) * Quaternion.AngleAxis(y, Vector3.right), Time.deltaTime * lerpSensitivity);
        cam.transform.position = head.transform.position;
        //보간 (추후 없애도 댐)
        head.transform.Rotate(-Vector3.forward, y, Space.Self);
        // 머리 끄덕끄덕
        player.transform.rotation = Quaternion.AngleAxis(x, Vector3.up);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
