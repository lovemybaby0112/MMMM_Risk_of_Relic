using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobAI : MonoBehaviour
{
    int state;
    public AIData data;
    GameObject[] player;
    [HideInInspector]
    public int doAI; //要做甚麼AI
    Animator animator;
    float hp;
    bool isDead;
    bool b_DoAI;
    Ray ray;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start()
    {
    }
    private void OnEnable()
    {
        if (this.gameObject.name == "Frightfly(Clone)") this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        state = 1;
        b_DoAI = true;
        isDead = false;
        this.transform.LookAt(player[0].transform.position);

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
        float info = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; //判斷動畫結束時間
        if (state == (int)MobState.BORN)
        {
            animator.Play("Spawn");
            state = (int)MobState.DOAI;
        }
        else if (state == (int)MobState.DOAI && b_DoAI == true)
        {
            doAI = SteeringBehavior.Seek(data);
            switch (doAI)
            {
                case 0:
                    data.doMove = false;
                    animator.SetBool("Run Forward", false);
                    animator.SetTrigger("Melee Attack");
                    break;
                case 1:
                    data.doMove = false;
                    animator.SetBool("Run Forward", false);
                    animator.SetTrigger("Ranged Attack");
                    break;
                case 2:
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run Forward In Place")) data.doMove = true;
                    else data.doMove = false;
                    SteeringBehavior.Move(data);
                    animator.SetBool("Run Forward", true);
                    break;
            }
            //if (SteeringBehavior.CollisiionAvoid(data) == false)
            //{

            //}
        }
        else if (state == (int)MobState.DIE && isDead == false)
        {
            if (this.gameObject.name == "Frightfly(Clone)")
            {
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            animator.SetTrigger("Die");
            isDead = true;
            b_DoAI = false;
        }
    }

    void ChangeDeadState()
    {
        hp = GetComponent<MobHp>().currentHealth;
        if (hp <= 0) state = (int)MobState.DIE;
    }

}

public enum MobState
{
    BORN =1,
    DOAI =2,
    DIE =3,
}

