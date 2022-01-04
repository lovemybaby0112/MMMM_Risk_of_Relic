using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    // Start is called before the first frame update

    public Camera m_camera;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    void Shooting()
    {
        RaycastHit hit;
        if(Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, range)) ;
        {
            Debug.Log(hit.transform.name);
        }
    }
}
