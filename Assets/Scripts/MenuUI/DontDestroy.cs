using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instace;

    private void Awake()
    {
        //避免從場景2回場景1時又多出一個背景音樂，故要使用實例化去判斷
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
