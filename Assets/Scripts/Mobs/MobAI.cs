using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobAI : MonoBehaviour
{
    public float state;
    public AIData data;
    GameObject[] player;
    public int doAI; //要做甚麼AI
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start()
    {
        this.transform.LookAt(player[0].transform.position);
        state = 1;
    }

    void Update()
    {
        data.targetPosition = player[0].transform.position;
        data.my = this.gameObject;
        FSM();
    }

    void FSM()
    {
        Debug.Log("狀態"+state);
        float info = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; //判斷動畫結束時間
        if (info >= 0.74f) state = (int)MobState.DOAI;
        if(state == (int)MobState.BORN) animator.Play("Spawn");
        else if (state == (int)MobState.DOAI)
        {           
            if (SteeringBehavior.CollisiionAvoid(data) == false)
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
                        SteeringBehavior.Move(data);
                        animator.SetBool("Run Forward", true);
                        break;
                }
            }
        }
        else if (state == (int)MobState.DIE)
        {
            Debug.Log("去死");
            animator.SetTrigger("Die");
        }
    }

    public void SetState(int mobState)
    {
        state = mobState;
    }

}

public enum MobState
{
    BORN =1,
    DOAI =2,
    DIE =3,
}

