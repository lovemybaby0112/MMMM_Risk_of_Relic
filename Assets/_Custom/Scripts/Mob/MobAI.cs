using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobAI : MonoBehaviour
{
    float state;
    public AIData data;
    GameObject[] player;
    int doAI; //要做甚麼AI
    Animator animator;

    bool startSeek = false;
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
        if(state == 1) animator.Play("Spawn");
        float info = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; //判斷動畫結束時間
        if(info >= 0.74f)
        {
            state = 2;
            doAI = SteeringBehavior.Seek(data);
            switch (doAI)
            {
                case 0:
                    animator.SetBool("Walk Forward", false);
                    animator.SetTrigger("Punch Attack");
                    break;
                case 1:
                    animator.SetBool("Walk Forward", false);
                    animator.SetTrigger("Breath Attack");
                    break;
                case 2:
                    SteeringBehavior.Move(data);
                    animator.SetBool("Walk Forward", true);
                    break;
            }
        }
       
    }

}
