using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobAI : MonoBehaviour
{
    //public GameObject player;
    public AIData data;
    GameObject[] player;

    Slider slider;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }

   
    void Update()
    {
        data.targetPosition = player[0].transform.position;
        data.my = this.gameObject;
        SteeringBehavior.Seek(data);
        SteeringBehavior.Move(data);
    }
}
