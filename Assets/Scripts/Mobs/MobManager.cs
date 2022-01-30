using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    
    private static MobManager MobManager_Ins; //singleton單例化
    public static MobManager Instance() { return MobManager_Ins; }
    public GameObject mob_FatherGameObject;//裝mob的父物件

    List<Mobs> mobsList;
    int count; //怪物陣列總長度

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
        //threeMobs = new Mobs[3]; //讓外面一次產生三隻
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
    public Mobs GetMob()
    {
        //List<Mobs> threeMobs = new List<Mobs>();
        Mobs mob = null;
        count = mobsList.Count;
        for (int i = 0; i < count; i++)
        {
            if (mobsList[i].onUsing == false)
            {
                mobsList[i].onUsing = true;
                mobsList[i].gameObject.transform.position = new Vector3(Random.Range(minX, maxX), 999.0f, Random.Range(minZ, maxZ));
                mob = mobsList[i];
                //threeMobs.Add(mobsList[i]);
                break;
            }
        }
        return mob;
        //for (int n = 0; n < 3; n++)
        //{

        //}

        //if (threeMobs[0] != null)
        //{
        //    threeMobs[0].gameObject.transform.position = new Vector3(Random.Range(minX, maxX), 999.0f, Random.Range(minZ, maxZ));//第一隻怪在box範圍內隨機出生
        //    float mob1_X = threeMobs[0].gameObject.transform.position.x;
        //    float mob1_Z = threeMobs[0].gameObject.transform.position.z;
        //    //後面兩隻怪生在第一隻怪旁邊
        //    if (threeMobs[1] != null) 
        //        threeMobs[1].gameObject.transform.position = new Vector3(Random.Range(mob1_X + 1, mob1_X + 5), 999.0f, Random.Range(mob1_Z + 1, mob1_Z + 5));
        //    else return null;
        //    if (threeMobs[2] != null)
        //        threeMobs[2].gameObject.transform.position = new Vector3(Random.Range(mob1_X - 1, mob1_X - 5), 999.0f, Random.Range(mob1_Z - 1, mob1_Z - 5));
        //    else return null;
        //    return threeMobs;
        //}
        //else return null;
    }

    /// <summary>
    /// 怪物死亡重置
    /// </summary>
    public void ResetMob(GameObject mob)
    {
        int iCount = mobsList.Count;
        for (int i = 0; i < iCount; i++)
        {
            if (mobsList[i].gameObject == mob)
            {
                mobsList[i].gameObject.SetActive(false);
                mobsList[i].onUsing = false;
                break;
            }
        }
    }
    #region 怪物出生
    /// <summary>
    /// 產卵(同時判定有沒有再有地板的地方出生)
    /// </summary>
    public bool Spawn()
    {
        Mobs mob;
        Ray ray; //判斷怪物有沒有在正確位置的射線
        RaycastHit hitInfo; //擊中的資訊
        int num = Random.Range(0, 10);
        Debug.Log(num);
        if (num < 6)
        {
            mob = GetMob();
            if (mob == null) return false;
            else
            {
                ray = new Ray(mob.gameObject.transform.position, Vector3.down);
                if (Physics.Raycast(ray, out hitInfo, 9999.0f, 1 << LayerMask.NameToLayer("Ground")))
                {
                    //重新賦Y值，Y值等於射線打到的點，套在怪物身上記得把+0.5f拔掉，因為pivot會在腳上
                    var mobP = mob.gameObject.transform.localPosition;
                    mobP.y = hitInfo.point.y;
                    mob.gameObject.transform.localPosition = mobP;
                    mob.gameObject.SetActive(true);
                    return true;
                }
                else
                { 
                    mob.onUsing = false;
                    return false;
                }
                //for (int i = 0; i < mob.Count; i++)
                //{

                //}
                //return true;
            }
        }
        else return false;
    }

    /// <summary>
    /// 執行產卵，幾秒後開始，每幾秒執行一次
    /// </summary>
    /// <param name="spawn"></param>
    public void DoSpawn(bool spawn)
    {
        if (spawn)
        {
            if (Spawn()) Spawn();
                //InvokeRepeating("Spawn", 1.0f, 1.0f);
        }
        //else CancelInvoke("Spawn"); //停止InvokeRepeating的方法
    }

    #endregion
}
