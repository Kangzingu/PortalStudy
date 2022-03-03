using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObjectMVPController : MonoBehaviour
{
    [Header("Player")]
    public Camera mainCam;

    [Header("Portal")]
    public Camera[] portalCam = new Camera[2];
    public GameObject[] portal = new GameObject[2];

    public GameObject passingObject;
    public GameObject duplicateObject;

    [Header("Matrix")]
    Matrix4x4 v;
    Matrix4x4 v1;
    Matrix4x4 v2;

    Matrix4x4 m1;
    Matrix4x4 m2;

    Matrix4x4 passingObjectM;

    Matrix4x4 editMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        v = mainCam.worldToCameraMatrix;
        v1 = portalCam[0].worldToCameraMatrix;
        v2 = portalCam[1].worldToCameraMatrix;

        m1 = portal[0].transform.localToWorldMatrix;
        m2 = portal[1].transform.localToWorldMatrix;

        editMat = Matrix4x4.Rotate(m2.rotation)*Matrix4x4.Rotate(Quaternion.Euler(Vector3.up * 180));
        editMat.SetTRS(new Vector3(m2.m03, m2.m13, m2.m23), editMat.rotation, m2.lossyScale);
        v2 = v * m1 * editMat.inverse;
        // 카메라 포탈기준 180도 회전, 위치조절
        editMat = Matrix4x4.Rotate(m1.rotation) * Matrix4x4.Rotate(Quaternion.Euler(Vector3.up * 180));
        editMat.SetTRS(new Vector3(m1.m03, m1.m13, m1.m23), editMat.rotation, m1.lossyScale);
        v1 = v * m2 * editMat.inverse;
        // 카메라 포탈기준 180도 회전, 위치조절

        portalCam[0].worldToCameraMatrix = v1;
        portalCam[1].worldToCameraMatrix = v2;
        // 카메라 변경 적용

        //**** 통과중인 물체에 대한 행렬, 통과중인 포탈 따라 달라짐 설정해줘야함 ****//
        bool isFirstPortal = true;
        if (isFirstPortal)
        {
            passingObjectM = v.inverse * v1 * passingObject.transform.localToWorldMatrix;
            duplicateObject.transform.localScale = ExtractScale(passingObjectM);
            duplicateObject.transform.rotation = ExtractRotation(passingObjectM);
            duplicateObject.transform.position = ExtractPosition(passingObjectM);
        }
        else
        {
            passingObjectM = v.inverse * v2 * passingObject.transform.localToWorldMatrix;
            duplicateObject.transform.localScale = ExtractScale(passingObjectM);
            duplicateObject.transform.rotation = ExtractRotation(passingObjectM);
            duplicateObject.transform.position = ExtractPosition(passingObjectM);
        }
        //**** 통과중인 물체에 대한 행렬, 통과중인 포탈 따라 달라짐 설정해줘야함 ****//
    }
    public static Quaternion ExtractRotation(Matrix4x4 matrix)
    {
        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;
        forward.z = matrix.m22;

        Vector3 upwards;
        upwards.x = matrix.m01;
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;

        return Quaternion.LookRotation(forward, upwards);
    }

    public static Vector3 ExtractPosition(Matrix4x4 matrix)
    {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }

    public static Vector3 ExtractScale(Matrix4x4 matrix)
    {
        Vector3 scale;
        scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
        scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
        scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
        return scale;
    }
}
