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
    public float mobMeleeAttackRange; //進戰攻擊距離
    public float mobSpellAttackRange; //法術攻擊距離
    public float mobSpeed; //怪物速度

    //~~~~~~~for 碰撞迴避~~~~~~~~~
    public float collProbeLength; //探針長度





}
