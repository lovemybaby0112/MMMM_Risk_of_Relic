using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData
{
    //~~~~~~~for seek~~~~~~~~~
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public GameObject my;
    [HideInInspector]
    public float turnForce;
    [HideInInspector]
    public float moveForce;
    public float maxMoveForce;
    [HideInInspector]
    public bool doMove;


    public float mobSpeed; //怪物速度
    public float mobAttackDistance; //怪物的攻擊距離


    
}
