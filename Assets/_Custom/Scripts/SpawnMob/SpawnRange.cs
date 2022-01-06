using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRange : MonoBehaviour
{
    Vector3 boxPosition;
    float minX;
    float maxX;
    float minZ;
    float maxZ;
    bool Spawn;

    void Start()
    {
        //將box的邊界值拿出來
        boxPosition = this.transform.position;
        minX = boxPosition.x - (this.transform.localScale.x - 1) / 2;
        maxX = boxPosition.x + (this.transform.localScale.x - 1) / 2;
        minZ = boxPosition.z - (this.transform.localScale.z - 1) / 2;
        maxZ = boxPosition.z + (this.transform.localScale.z - 1) / 2;
        Spawn = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MobManager.Instance().minX = minX;
            MobManager.Instance().maxX = maxX;
            MobManager.Instance().minZ = minZ;
            MobManager.Instance().maxZ = maxZ;
            Spawn = true;
            MobManager.Instance().DoSpawn(Spawn);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Spawn = false;
            MobManager.Instance().DoSpawn(Spawn);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, this.transform.localScale);
    }
}
