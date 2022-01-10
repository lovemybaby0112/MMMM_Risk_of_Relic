using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    
    private static MobManager MobManager_Ins; //singleton單例化
    public static MobManager Instance() { return MobManager_Ins; }
    public GameObject mob_FatherGameObject;//裝mob的父物件

    List<Mobs> mobsList;
    int count; //怪物陣列總長度
    Mobs[] threeMobs; //裝每次產生三隻怪的陣列

    //怪物生成位置座標的大小數值
    [HideInInspector]
    public float minX, maxX; 
    [HideInInspector]
    public float minZ, maxZ;

    /// <summary>
    /// Constructor
    /// </summary>
    public MobManager()
    {
        MobManager_Ins = this;
        mobsList = new List<Mobs>();
        threeMobs = new Mobs[3]; //讓外面一次產生三隻
    }

    /// <summary>
    /// 創建怪物
    /// </summary>
    /// <param name="mobName">Prefab的名稱</param>
    public void CreateMobs(string mobName)
    {
        GameObject mobGameObject = null;

        Object getMobPrefab = Resources.Load($"Mobs/{mobName}"); //拿到場景上屬於這個名稱的prefab
        mobGameObject = Instantiate(getMobPrefab) as GameObject;  //轉型
        mobGameObject.SetActive(false);
        mobGameObject.transform.parent = mob_FatherGameObject.transform; //放入父物件
        //將他放進怪物List裡面
        switch (mobName)
        {
            case "Mushroom": 
                Mobs mushroom = new Mushroom();
                mushroom.gameObject = mobGameObject;
                mushroom.onUsing = false;
                mobsList.Add(mushroom);
                break;
        }
    }

    /// <summary>
    /// 得到未再使用中的怪物物件，設定其位置，並把其設定為使用中
    /// </summary>
    /// <returns></returns>
    public Mobs[] GetMob()
    {
        count = mobsList.Count;
        GameObject mob = null;
        for (int n = 0; n < threeMobs.Length; n++)
        {
            for (int i = 0; i < count; i++)
            {
                if (mobsList[i].onUsing == false)
                {
                    mobsList[i].onUsing = true;
                    //mob = mobsList[i].gameObject;
                    threeMobs[n] = mobsList[i];
                    break;
                }
            }
            //if (mob == null) return null;
            
        }
        threeMobs[0].gameObject.transform.position = new Vector3(Random.Range(minX, maxX), 50.0f, Random.Range(minZ, maxZ));//第一隻怪在box範圍內隨機出生
        float mob1_X = threeMobs[0].gameObject.transform.position.x;
        float mob1_Z = threeMobs[0].gameObject.transform.position.z;
        //後面兩隻怪生在第一隻怪旁邊
        threeMobs[1].gameObject.transform.position = new Vector3(Random.Range(mob1_X + 1, mob1_X + 5), 50.0f, Random.Range(mob1_Z + 1, mob1_Z + 5));
        threeMobs[2].gameObject.transform.position = new Vector3(Random.Range(mob1_X - 1, mob1_X - 5), 50.0f, Random.Range(mob1_Z - 1, mob1_Z - 5));          
        if (threeMobs == null) return null;
        return threeMobs;
    }

    /// <summary>
    /// 把不在正確位置的怪物OnUse設成false
    /// </summary>
    /// <param name="gameObject"></param>
    //public void SetMobOnUsingFalse(GameObject gameObject)
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        if (mobsList[i].gameObject == gameObject) mobsList[i].onUsing = false;
    //    }
    //}

    #region 怪物出生
    /// <summary>
    /// 產卵(同時判定有沒有再有地板的地方出生)
    /// </summary>
    public void Spawn()
    {
        Mobs[] mob;
        Ray ray; //判斷怪物有沒有在正確位置的射線
        RaycastHit hitInfo; //擊中的資訊
        int num = Random.Range(0, 10);
        Debug.Log(num);
        if (num < 6)
        {
            mob = GetMob();
            for (int i = 0; i < mob.Length; i++)
            {
                ray = new Ray(mob[i].gameObject.transform.position, Vector3.down);
                if (Physics.Raycast(ray, out hitInfo, 9999.0f, 1 << LayerMask.NameToLayer("Terrain")))
                {
                    //重新賦Y值，Y值等於射線打到的點，套在怪物身上記得把+0.5f拔掉，因為pivot會在腳上
                    var mobP = mob[i].gameObject.transform.localPosition;
                    mobP.y = hitInfo.point.y;
                    mob[i].gameObject.transform.localPosition = mobP;                    
                    mob[i].gameObject.SetActive(true);
                }
                else mob[i].onUsing = false;
            }
        }
    }

    /// <summary>
    /// 執行產卵，幾秒後開始，每幾秒執行一次
    /// </summary>
    /// <param name="spawn"></param>
    public void DoSpawn(bool spawn)
    {
        if (spawn)
        {
            InvokeRepeating("Spawn", 3.0f, 1.0f);
        }
        else CancelInvoke("Spawn"); //停止InvokeRepeating的方法
    }

    #endregion
}
