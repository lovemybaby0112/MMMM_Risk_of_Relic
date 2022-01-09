using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIData
{
    //~~~~~~~for seek~~~~~~~~~
    [HideInInspector]
    public Vector3 targetPosition; //�ؼЮy��
    [HideInInspector]
    public GameObject my; //�ۤv
    [HideInInspector]
    public float turnForce; //��V�O
    public float maxTurnForce; //�̤j��V�O
    [HideInInspector]
    public float moveForce; //���ʳt�v
    [HideInInspector]
    public bool doMove; //����
    [HideInInspector]
    public Vector3 myCurrentVector; //���e�t�v



    public float mobSpeed; //�Ǫ��t��
    public float mobAttackRamge; //�Ǫ��������Z��


    
}