using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instace;

    private void Awake()
    {
        //�קK�q����2�^����1�ɤS�h�X�@�ӭI�����֡A�G�n�ϥι�Ҥƥh�P�_
        //https://blog.csdn.net/LLLLL__/article/details/109247621
        if (Instace != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instace = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        //if(this.gameObject != null)
        //{
            
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
