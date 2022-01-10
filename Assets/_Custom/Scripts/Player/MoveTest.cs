using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public GameObject gb; //給一個要控制的物件
    void Start()
    {
        gb = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float fh = Input.GetAxis("Horizontal");
        float fv = Input.GetAxis("Vertical");

        Vector3 vMove = gb.transform.forward * fv; //前進的話就*這個值
        vMove += gb.transform.right * fh; //+這行就變成左右橫移
                                          //這邊使用上面任一平移方法給速度，移動
        Vector3 vector3 = gb.transform.position + vMove * 10f * Time.deltaTime;
        gb.transform.position = vector3;
        transform.Rotate(0.0f, fh, 0.0f); //轉向
    }
}
