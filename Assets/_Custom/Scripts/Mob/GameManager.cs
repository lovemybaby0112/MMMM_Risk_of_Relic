using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        #region �ЫةǪ�����
        string mobname = "Mushroom";
        for (int i = 0; i < 10; i++)
        {
            MobManager.Instance().CreateMobs(mobname);
        }
        #endregion
    }

    void Update()
    {
        
    }
}
