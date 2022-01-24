using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public Transform cinemachine;

    [SerializeField]
    private LayerMask attackColliderLayerMask = new LayerMask();
    [SerializeField]
    private Transform debugTran;

    private ThirdPersonController ThirdPersonController;
    private StarterAssetsInputs StarterAssetsInputs;
    Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
    private void Awake()
    {
        //StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        //ThirdPersonController = GetComponent<ThirdPersonController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
            
        }
        Debug.DrawLine(cinemachine.position, screenCenterPoint, Color.red, 0.1f);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.1f);
    }

    void Shoot()
    {
        RaycastHit Hit;

        Physics.Raycast(ray, out Hit, 999f);
        
            GameObject hitObj = Hit.collider.gameObject;
            Debug.Log(hitObj);
        

        
        
        
    }
}
