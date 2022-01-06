using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior
{

    static public void Move(AIData data)
    {
        if (data.doMove == false) return; //不做事
        if (data.moveForce > data.maxMoveForce) data.moveForce = data.maxMoveForce; //如果超過最大轉向力=最大轉向力
        else if(data.moveForce < -data.maxMoveForce) data.moveForce = -data.maxMoveForce;//如果超過最小轉向力=最小轉向力


    }
    public bool Seek(AIData data, Action action)
    {
        Vector3 target = data.target.transform.position; //取得目標位置
        Vector3 velocity = target - data.my.transform.position; //目標位置-自己的位置得到向量
        velocity.y = 0.0f;//模組pivot都在腳下就不用這行

        float distance = velocity.magnitude; //將向量轉純量(變長度)

        if (distance < data.mobSpeed + 0.001f) //0.001f是偏差值，有其他怪物之後記得改成distance <= AIData.mobAttackDistance + 0.001f
        {
            Vector3 finalPosition = target;
            data.doMove = false;
            return false; //停止Seek?開始在外面做AI?
            //action();//在裡面做AI??
        }

        Vector3 myForward = data.my.transform.forward;
        Vector3 myRight = data.my.transform.right;
        velocity.Normalize();//將其轉為單位向量

        //計算加減速
        float vecDotForward = Vector3.Dot(velocity, myForward);
        if(vecDotForward > 0.96f)
        {
            //如果與面對方向幾乎平行於目標，就設定為平行，然後全力往前
            vecDotForward = 1.0f;
            data.turnForce = 0.0f;//轉向力規零
        }
        else
        {
            float vecDotRight = Vector3.Dot(velocity, myRight); //計算轉向、轉向力
            if(vecDotForward < 0.0f) //如果目標在背後
            {
                //將左右轉向力都設為最大
                if(vecDotRight > 0.0f) vecDotRight = 1.0f;
                else vecDotRight = -1.0f;
            }
            if (distance < 3.0f) vecDotRight *= (distance / 3.0f + 1.0f); //距離越短就判斷轉向力是否要減少，還是要維持最大一直轉
            data.turnForce = vecDotRight;
        }

        data.doMove = true;
        return true;
        //應該不用?這段是靠近時減速?
        //if (distance < 3.0f)
        //{
        //    if (AIData.mobSpeed > 0.1f) AIData.moveForce = (1.0f - distance / 3.0f) * 5.0f;
        //    else AIData.moveForce = vecDotForward * 100.0f;
        //}
        //else AIData.moveForce = 100.0f;
    }
}
