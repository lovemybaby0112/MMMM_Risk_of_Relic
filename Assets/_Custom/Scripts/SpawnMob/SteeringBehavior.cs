using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior
{

    static public void Move(AIData data)
    {
        if (data.doMove == false) return; //������
        if (data.moveForce > data.maxMoveForce) data.moveForce = data.maxMoveForce; //�p�G�W�L�̤j��V�O=�̤j��V�O
        else if(data.moveForce < -data.maxMoveForce) data.moveForce = -data.maxMoveForce;//�p�G�W�L�̤p��V�O=�̤p��V�O


    }
    public bool Seek(AIData data, Action action)
    {
        Vector3 target = data.target.transform.position; //���o�ؼЦ�m
        Vector3 velocity = target - data.my.transform.position; //�ؼЦ�m-�ۤv����m�o��V�q
        velocity.y = 0.0f;//�Ҳ�pivot���b�}�U�N���γo��

        float distance = velocity.magnitude; //�N�V�q��¶q(�ܪ���)

        if (distance < data.mobSpeed + 0.001f) //0.001f�O���t�ȡA����L�Ǫ�����O�o�令distance <= AIData.mobAttackDistance + 0.001f
        {
            Vector3 finalPosition = target;
            data.doMove = false;
            return false; //����Seek?�}�l�b�~����AI?
            //action();//�b�̭���AI??
        }

        Vector3 myForward = data.my.transform.forward;
        Vector3 myRight = data.my.transform.right;
        velocity.Normalize();//�N���ର���V�q

        //�p��[��t
        float vecDotForward = Vector3.Dot(velocity, myForward);
        if(vecDotForward > 0.96f)
        {
            //�p�G�P�����V�X�G�����ؼСA�N�]�w������A�M����O���e
            vecDotForward = 1.0f;
            data.turnForce = 0.0f;//��V�O�W�s
        }
        else
        {
            float vecDotRight = Vector3.Dot(velocity, myRight); //�p����V�B��V�O
            if(vecDotForward < 0.0f) //�p�G�ؼЦb�I��
            {
                //�N���k��V�O���]���̤j
                if(vecDotRight > 0.0f) vecDotRight = 1.0f;
                else vecDotRight = -1.0f;
            }
            if (distance < 3.0f) vecDotRight *= (distance / 3.0f + 1.0f); //�Z���V�u�N�P�_��V�O�O�_�n��֡A�٬O�n�����̤j�@����
            data.turnForce = vecDotRight;
        }

        data.doMove = true;
        return true;
        //���Ӥ���?�o�q�O�a��ɴ�t?
        //if (distance < 3.0f)
        //{
        //    if (AIData.mobSpeed > 0.1f) AIData.moveForce = (1.0f - distance / 3.0f) * 5.0f;
        //    else AIData.moveForce = vecDotForward * 100.0f;
        //}
        //else AIData.moveForce = 100.0f;
    }
}
