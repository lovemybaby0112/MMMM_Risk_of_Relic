using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        #region �ЫةǪ�����
        string mobname = "mob";
        for (int i = 0; i < 100; i++)
        {
            MobManager.Instance().CreateMobs(mobname);
        }
        #endregion
    }

    void Update()
    {
        
    }
}
