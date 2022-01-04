using UnityEngine;

public class CameraFlowMouse: MonoBehaviour
{
    public float mouseSensitivity = 100f;    //·Æ¹«ÆF±Ó
    public Transform playerBody;
    float xRotation ;

    

    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
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
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Debug.Log(mouseY);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }


    
}
