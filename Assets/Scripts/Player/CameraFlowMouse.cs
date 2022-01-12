using UnityEngine;

public class CameraFlowMouse : MonoBehaviour
{
    public float mouseSensitivity = 100f;    //·Æ¹«ÆF±Ó
    public Transform playerBody;
    public Camera camara;
    float xRotation;

    //public Transform camera;
    //public Transform cameraHighest;
    //public Transform cameraLowest;

   

    public float journeyTime = 1.0f;
    private float startTime;





    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camara = GetComponent<Camera>();

    }


    void Update()
    {
        FlowMouse();
       
    }



    void FlowMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //Debug.Log(mouseY);
        //Debug.Log(xRotation);
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);






        //Vector3 center = cameraHighest.position + cameraLowest.position * 0.5f;

        //    center -= new Vector3(0, 1, 0);
        //    Vector3 riseRelCenter = cameraHighest.position - center;
        //    Vector3 setRelCenter = cameraLowest.position - center;

        //    float fracComplete = (Time.time - startTime) / journeyTime;
        //    transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        //    transform.position += center;
        



    }
}
