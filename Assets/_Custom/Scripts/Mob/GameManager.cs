using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        #region 創建怪物物件
        string mobname = "Mushroom";
        for (int i = 0; i < 3; i++)
        {
            MobManager.Instance().CreateMobs(mobname);
        }
        #endregion
    }

    void Update()
    {
        
    }
}
