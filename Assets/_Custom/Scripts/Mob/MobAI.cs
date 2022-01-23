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
        float info = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; //判斷動畫結束時間
        if (info >= 0.74f) state = 2;
        if(state == 1) animator.Play("Spawn");
        if (state == 2)
        {
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

    private void OnDrawGizmos()
    {
        if(data != null)
        {
            Vector3 newPivot = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(newPivot, newPivot + this.transform.forward * data.collProbeLength);
            Gizmos.color = Color.red;
            Vector3 vLastTemp = -this.transform.right;
            for (int i = 1; i < 180; i++)
            {
                Vector3 vTemp = Quaternion.Euler(0.0f, 1.0f * i, 0.0f) * -this.transform.right;
                Vector3 vStart = newPivot + vLastTemp * data.collProbeLength;
                Vector3 vEnd = newPivot + vTemp * data.collProbeLength;
                vLastTemp = vTemp;
                Gizmos.DrawLine(vStart, vEnd);
            }
        }
    }

}
