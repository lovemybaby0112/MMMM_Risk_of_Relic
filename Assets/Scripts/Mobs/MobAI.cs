using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobAI : MonoBehaviour
{
    int state;
    public AIData data;
    GameObject[] player;
    public int doAI; //要做甚麼AI
    Animator animator;
    float hp;
    bool isDead;
    bool b_DoAI;
    private void Awake()
    {

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start()
    {
        b_DoAI = true;
        isDead = false;
        this.transform.LookAt(player[0].transform.position);
        state = 1;

    }

    void Update()
    {

        data.targetPosition = player[0].transform.position;
        data.my = this.gameObject;
        FSM();
        ChangeDeadState();
    }

    void FSM()
    {
        Debug.Log("狀態"+ state);   
        if (state == (int)MobState.BORN)
        { 
            animator.Play("Spawn");
            float info = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; //判斷動畫結束時間
            if (info >= 0.74f) state = (int)MobState.DOAI;
        }
        else if (state == (int)MobState.DOAI && b_DoAI == true)
        {
            doAI = SteeringBehavior.Seek(data);
            switch (doAI)
            {
                case 0:
                    data.doMove = false;
                    animator.SetBool("Run Forward", false);
                    animator.SetTrigger("Punch Attack");
                    break;
                case 1:
                    data.doMove = false;
                    animator.SetBool("Run Forward", false);
                    animator.SetTrigger("Breath Attack");
                    break;
                case 2:
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run Forward In Place"))
                    {
                        data.doMove = true;
                    }
                    else data.doMove = false;
                    SteeringBehavior.Move(data);
                    //data.doMove = true;
                    animator.SetBool("Run Forward", true);
                    break;
            }
            //if (SteeringBehavior.CollisiionAvoid(data) == false)
            //{

            //}
        }
        else if (state == (int)MobState.DIE && isDead == false)
        {
            animator.SetTrigger("Die");
            isDead = true;
            b_DoAI = false;
            state = 4;
        }
    }

    void ChangeDeadState()
    {
        hp = GetComponent<MobHp>().currentHealth;
        if(hp <= 0)
        {
            state = (int)MobState.DIE;
        }
    }
}

public enum MobState
{
    BORN =1,
    DOAI =2,
    DIE =3,
}

