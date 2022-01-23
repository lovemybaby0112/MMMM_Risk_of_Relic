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
            data.doMove = false;
            return (int)DoAI.MeleeAttack;
        }
        else if (distance <=  data.mobSpellAttackRange + 0.001f) //0.001f是偏差值，有其他怪物之後記得改成distance <= AIData.mobAttackDistance + 0.001f
        {
            data.doMove = false;
            return (int)DoAI.SpellAttack;
            // Vector3 finalPosition = data.my.transform.position;
            //return false; //停止Seek?開始在外面做AI?
            //action();//在裡面做AI??
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
        data.doMove = true;
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
}
