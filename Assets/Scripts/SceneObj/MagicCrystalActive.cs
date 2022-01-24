using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCrystalActive : MonoBehaviour
{
    public GameObject box;
    public GameObject crystal;

    bool beUse = false;
    private void OnTriggerEnter(Collider other)
    {
        box.SetActive(true);
        beUse = true;
    }

    private void OnTriggerExit(Collider other)
    {
        box.SetActive(false);
        beUse = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            crystal.SetActive(false);
            transform.GetComponent<SphereCollider>().enabled = false;
        }
    }
    void useCrystal()
    {

    }
}
