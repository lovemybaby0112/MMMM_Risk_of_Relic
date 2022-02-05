using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior
{
    static public void Move(AIData data)
    {
        if (data.doMove == false) return; //������
        else
        {          
            Transform my = data.my.transform;

            Vector3 myPosition = data.my.transform.position;
            Vector3 mForward = my.forward;
            Vector3 mRight = my.right;
            Vector3 goDirection = data.myCurrentVector;

            if (data.turnForce > data.maxTurnForce) data.turnForce = data.maxTurnForce; //�p�G�W�L�̤j��V�O=�̤j��V�O
            else if (data.turnForce < -data.maxTurnForce) data.turnForce = -data.maxTurnForce;//�p�G�W�L�̤p��V�O=�̤p��V�O

            //�����N��AI-2���s2021_12_01_18_33_01_361 21:00�ϸ�
            goDirection = goDirection + mRight * data.turnForce;//�N�ۤv��forward+right*��V�O�A�N�|��right����
            my.forward = goDirection;//�N�����V�אּRight�����X�h�����ӳ��V�q

            myPosition = myPosition + my.forward * data.moveForce * Time.deltaTime; //����
            my.position = myPosition;
        }
    }

    static public int Seek(AIData data)//Action action
    {
        Vector3 target = data.targetPosition; //���o�ؼЦ�m       
        Vector3 velocity = target - data.my.transform.position; //�ؼЦ�m-�ۤv����m�o��V�q
        velocity.y = 0.0f;//�Ҳ�pivot���b�}�U�N���γo��
        float distance = velocity.magnitude; //�N�V�q��¶q(�ܪ���)


        if (distance <= data.mobMeleeAttackRange + 0.001f)
        {
            data.my.transform.LookAt(target);
            //data.doMove = false;
            return (int)DoAI.MeleeAttack;
        }
        else if (distance <=  data.mobSpellAttackRange + 0.001f) //0.001f�O���t�ȡA����L�Ǫ�����O�o�令distance <= AIData.mobAttackDistance + 0.001f
        {
            data.my.transform.LookAt(target);
            //data.doMove = false;
            return (int)DoAI.SpellAttack;
        }

        Vector3 myForward = data.my.transform.forward;
        Vector3 myRight = data.my.transform.right;
        velocity.Normalize();//�N���ର���V�q
        data.myCurrentVector = myForward; //�ثe��V(�t�v)�O�����V
        data.moveForce = data.mobSpeed;
        //�p��[��t
        float vecDotForward = Vector3.Dot(velocity, myForward);
        if(vecDotForward > 0.96f)
        {
            //�p�G�P�����V�X�G�����ؼСA�N�]�w������A�M����O���e            
            vecDotForward = 1.0f;
            data.turnForce = 0.0f;//��V�O�W�s
            data.myCurrentVector = velocity; //�����V�N�אּ���ؼЪ���V(�t�v)
        }
        else
        {
            data.moveForce = data.mobSpeed;
            float vecDotRight = Vector3.Dot(velocity, myRight); //�p����V�B��V�O
            if(vecDotForward < 0.0f) //�p�G�ؼЦb�I��
            {
                data.moveForce = 0.01f;
                //�N���k��V�O���]���̤j
                if(vecDotRight > 0.0f) vecDotRight = 1.0f;
                else vecDotRight = -1.0f;
            }
            if (distance < 3.0f) vecDotRight *= (distance / 3.0f + 1.0f); //�V������I����V�O�V�j�A�Ϩ�b�ѤU�Z���බ�Q��F�ؼ�
            data.turnForce = vecDotRight;
        }
        //data.doMove = true;
        return (int)DoAI.Move;
       
        //return true;
        //���Ӥ���?�o�q�O�a��ɴ�t?
        //if (distance < 3.0f)
        //{
        //    if (AIData.mobSpeed > 0.1f) AIData.moveForce = (1.0f - distance / 3.0f) * 5.0f;
        //    else AIData.moveForce = vecDotForward * 100.0f;
        //}
        //else AIData.moveForce = 100.0f;
    }
    public enum DoAI
    {
        MeleeAttack = 0,
        SpellAttack = 1,
        Move = 2,
    }

    //static public bool CollisiionAvoid(AIData data)
    //{
    //    List<Obstacle> avoidTargets = GameManager.gameManagerIns.GetObstacles(); //����Ҧ���ê��
    //    Transform myTrans = data.my.transform;
    //    Vector3 myPos = myTrans.position;
    //    Vector3 myForward = myTrans.forward;
    //    data.myCurrentVector = myForward;
    //    Vector3 vec;
    //    float fFinalDotDist;
    //    float fFinalProjDist;
    //    Vector3 vFinalVec = Vector3.forward;
    //    Obstacle oFinal = null;
    //    float fDist = 0.0f;
    //    float fDot = 0.0f;
    //    float fFinalDot = 0.0f;
    //    int iCount = avoidTargets.Count;
    //    //Debug.Log(avoidTargets[0]);

    //    float fMinDist = 10000.0f;
    //    for (int i = 0; i < iCount; i++)
    //    {
    //        vec = avoidTargets[i].transform.position - myPos;

    //        vec.y = 0.0f;
    //        fDist = vec.magnitude;
    //        if (fDist > data.myProbeLength + avoidTargets[i].myRadius)
    //        {
    //            avoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
    //            continue;
    //        }

    //        vec.Normalize();
    //        fDot = Vector3.Dot(vec, myForward);
    //        if (fDot < 0)
    //        {
    //            avoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
    //            continue;
    //        }
    //        else if (fDot > 1.0f)
    //        {
    //            fDot = 1.0f;
    //        }
    //        avoidTargets[i].m_eState = Obstacle.eState.INSIDE_TEST;
    //        float fProjDist = fDist * fDot;
    //        float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
    //        if (fDotDist > avoidTargets[i].myRadius + data.myRadius)
    //        {
    //            continue;
    //        }

    //        if (fDist < fMinDist)
    //        {
    //            fMinDist = fDist;
    //            fFinalDotDist = fDotDist;
    //            fFinalProjDist = fProjDist;
    //            vFinalVec = vec;
    //            oFinal = avoidTargets[i];
    //            fFinalDot = fDot;
    //        }

    //    }

    //    if (oFinal != null)
    //    {
    //        Vector3 vCross = Vector3.Cross(myForward, vFinalVec);
    //        float fTurnMag = Mathf.Sqrt(1.0f - fFinalDot * fFinalDot);
    //        if (vCross.y > 0.0f)
    //        {
    //            fTurnMag = -fTurnMag;
    //        }
    //        data.turnForce = fTurnMag;

    //        float fTotalLen = data.myProbeLength + oFinal.myRadius;
    //        float fRatio = fMinDist / fTotalLen;
    //        if (fRatio > 1.0f)
    //        {
    //            fRatio = 1.0f;
    //        }
    //        fRatio = 1.0f - fRatio;
    //        data.moveForce = -fRatio;
    //        oFinal.m_eState = Obstacle.eState.COL_TEST;
    //        data.doCol = true;
    //        data.doMove = true;
    //        return true;
    //    }
    //    data.doCol = false;
    //    return false;
    //}
}
