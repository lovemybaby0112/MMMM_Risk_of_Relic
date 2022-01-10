using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public GameObject gb; //���@�ӭn�������
    void Start()
    {
        gb = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float fh = Input.GetAxis("Horizontal");
        float fv = Input.GetAxis("Vertical");

        Vector3 vMove = gb.transform.forward * fv; //�e�i���ܴN*�o�ӭ�
        vMove += gb.transform.right * fh; //+�o��N�ܦ����k�
                                          //�o��ϥΤW�����@������k���t�סA����
        Vector3 vector3 = gb.transform.position + vMove * 10f * Time.deltaTime;
        gb.transform.position = vector3;
        transform.Rotate(0.0f, fh, 0.0f); //��V
    }
}
