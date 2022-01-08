using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIData
{
    //~~~~~~~for seek~~~~~~~~~
    [HideInInspector]
    public Vector3 targetPosition; //目標座標
    [HideInInspector]
    public GameObject my; //自己
    [HideInInspector]
    public float turnForce; //轉向力
    public float maxTurnForce; //最大轉向力
    [HideInInspector]
    public float moveForce; //移動速率
    [HideInInspector]
    public bool doMove; //移動
    [HideInInspector]
    public Vector3 myCurrentVector; //當前速率



    public float mobSpeed; //怪物速度
    public float mobAttackRamge; //怪物的攻擊距離


    
}
