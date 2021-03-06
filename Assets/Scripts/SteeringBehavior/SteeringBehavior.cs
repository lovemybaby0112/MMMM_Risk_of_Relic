using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior
{
    static public void Move(AIData data)
    {
        if (data.doMove == false) return; //不做事
        else
        {          
            Transform my = data.my.transform;

            Vector3 myPosition = data.my.transform.position;
            Vector3 mForward = my.forward;
            Vector3 mRight = my.right;
            Vector3 goDirection = data.myCurrentVector;

            if (data.turnForce > data.maxTurnForce) data.turnForce = data.maxTurnForce; //如果超過最大轉向力=最大轉向力
            else if (data.turnForce < -data.maxTurnForce) data.turnForce = -data.maxTurnForce;//如果超過最小轉向力=最小轉向力

            //不懂就看AI-2錄製2021_12_01_18_33_01_361 21:00圖解
            goDirection = goDirection + mRight * data.turnForce;//將自己的forward+right*轉向力，就會把right延長
            my.forward = goDirection;//將面對方向改為Right延伸出去的那個單位向量

            myPosition = myPosition + my.forward * data.moveForce * Time.deltaTime; //移動
            my.position = myPosition;
        }
    }

    static public int Seek(AIData data)//Action action
    {
        Vector3 target = data.targetPosition; //取得目標位置       
        Vector3 velocity = target - data.my.transform.position; //目標位置-自己的位置得到向量
        velocity.y = 0.0f;//模組pivot都在腳下就不用這行
        float distance = velocity.magnitude; //將向量轉純量(變長度)


        if (distance <= data.mobMeleeAttackRange + 0.001f)
        {
            data.my.transform.LookAt(target);
            //data.doMove = false;
            return (int)DoAI.MeleeAttack;
        }
        else if (distance <=  data.mobSpellAttackRange + 0.001f) //0.001f是偏差值，有其他怪物之後記得改成distance <= AIData.mobAttackDistance + 0.001f
        {
            data.my.transform.LookAt(target);
            //data.doMove = false;
            return (int)DoAI.SpellAttack;
        }

        Vector3 myForward = data.my.transform.forward;
        Vector3 myRight = data.my.transform.right;
        velocity.Normalize();//將其轉為單位向量
        data.myCurrentVector = myForward; //目前方向(速率)是面對方向
        data.moveForce = data.mobSpeed;
        //計算加減速
        float vecDotForward = Vector3.Dot(velocity, myForward);
        if(vecDotForward > 0.96f)
        {
            //如果與面對方向幾乎平行於目標，就設定為平行，然後全力往前            
            vecDotForward = 1.0f;
            data.turnForce = 0.0f;//轉向力規零
            data.myCurrentVector = velocity; //面對方向就改為往目標的方向(速率)
        }
        else
        {
            data.moveForce = data.mobSpeed;
            float vecDotRight = Vector3.Dot(velocity, myRight); //計算轉向、轉向力
            if(vecDotForward < 0.0f) //如果目標在背後
            {
                data.moveForce = 0.01f;
                //將左右轉向力都設為最大
                if(vecDotRight > 0.0f) vecDotRight = 1.0f;
                else vecDotRight = -1.0f;
            }
            if (distance < 3.0f) vecDotRight *= (distance / 3.0f + 1.0f); //越接近終點時轉向力越大，使其在剩下距離能順利到達目標
            data.turnForce = vecDotRight;
        }
        //data.doMove = true;
        return (int)DoAI.Move;
       
        //return true;
        //應該不用?這段是靠近時減速?
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
    //    List<Obstacle> avoidTargets = GameManager.gameManagerIns.GetObstacles(); //接住所有障礙物
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
