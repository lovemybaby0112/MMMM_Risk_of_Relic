using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distanceAway = 1.7f;
    public float distanceUp = 1.3f;
    public float smooth = 2f; // how smooth the camera movement is
    private Vector3 m_TargetPosition; // the position the camera is trying to be in)
    Transform follow; //the position of Player
    Vector3 relCameraPos;

    void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
        relCameraPos = transform.position - follow.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        CameraControl();
        //// setting the target position to be the correct offset from the
        //m_TargetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
        //// making a smooth transition between it's current position and the position it wants to be in
        //transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);
        //// make sure the camera is looking the right way!
        //transform.LookAt(follow);
    }

    public void CameraControl()
    {
        float cam_h = Input.GetAxis("Mouse X");
        float cam_v = Input.GetAxis("Mouse Y");
        transform.position = relCameraPos + follow.position;
        transform.RotateAround(follow.position, Vector3.up, cam_h * smooth);

        float angleX = transform.rotation.eulerAngles.x;
        float nextAngleX = -cam_v * smooth + angleX;

        if (nextAngleX >= 360f)
        {
            nextAngleX -= 360f;
        }
        if ((nextAngleX < 60f) || (nextAngleX <= 360f && nextAngleX >= 300f))
            transform.RotateAround(follow.position, transform.right, -cam_v * smooth);

        relCameraPos = transform.position - follow.position;
    }
}
