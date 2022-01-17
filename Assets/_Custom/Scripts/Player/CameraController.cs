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
    private Vector3 relCameraPos;
    private Vector3 camera_offset;
    private RaycastHit hit;
    private float dis;

    void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
        relCameraPos = transform.position - follow.position;
        dis = relCameraPos.magnitude;
        camera_offset = transform.localPosition;
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
        var dis = relCameraPos.magnitude;
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
        //if (Physics.Linecast(follow.position, transform.position, out hit))
        //{
        //    transform.position = new Vector3(0, 0, Vector3.Distance(follow.position, hit.point));
        //    //transform.position =  hit.point + transform.position * 0.01f;
        //    transform.LookAt(follow);
        //}
        //else
        //{
        //    transform.position = Vector3.Lerp(transform.position, camera_offset, Time.deltaTime);
        //    Debug.Log(transform.position);

        //}

        relCameraPos = transform.position - follow.position;
    }
}
