using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MobManager : MonoBehaviour
{

    private static MobManager MobManager_Ins; //singleton單例化
    public static MobManager Instance() { return MobManager_Ins; }
    public GameObject mob_FatherGameObject;//裝mob的父物件

    List<Mobs> mushroomList;
    List<Mobs> frightflyList;
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
        mushroomList = new List<Mobs>();
        frightflyList = new List<Mobs>();
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
                mushroomList.Add(mushroom);
                break;
            case "Frightfly":
                Mobs frightfly = new Frightfly();
                frightfly.gameObject = mobGameObject;
                frightfly.onUsing = false;
                frightflyList.Add(frightfly);
                break;
        }
    }

    /// <summary>
    /// 得到未再使用中的怪物物件，設定其位置，並把其設定為使用中
    /// </summary>
    /// <returns></returns>
    public Mobs GetMob(string mobname)
    {
        Mobs mob = null;
        switch (mobname)
        {
            case "Mushroom":
                count = mushroomList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (mushroomList[i].onUsing == false)
                    {
                        mushroomList[i].onUsing = true;
                        mushroomList[i].gameObject.transform.position = new Vector3(Random.Range(minX, maxX), 999.0f, Random.Range(minZ, maxZ));
                        mob = mushroomList[i];
                        break;
                    }
                }
                break;
            case "Frightfly":
                count = frightflyList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (frightflyList[i].onUsing == false)
                    {
                        frightflyList[i].onUsing = true;
                        frightflyList[i].gameObject.transform.position = new Vector3(Random.Range(minX, maxX), 999.0f, Random.Range(minZ, maxZ));
                        mob = frightflyList[i];
                        break;
                    }
                }
                break;
        }
        return mob;
    }


    /// <summary>
    /// 怪物死亡重置
    /// </summary>
    public void ResetMob(GameObject mob, string mobname)
    {
        switch (mobname)
        {
            case "Mushroom(Clone)":
                int iCount = mushroomList.Count;
                for (int i = 0; i < iCount; i++)
                {
                    if (mushroomList[i].gameObject == mob)
                    {
                        mushroomList[i].gameObject.SetActive(false);
                        mushroomList[i].onUsing = false;
                        break;
                    }
                }
                break;
            case "Frightfly(Clone)":
                iCount = frightflyList.Count;
                for (int i = 0; i < iCount; i++)
                {
                    if (frightflyList[i].gameObject == mob)
                    {
                        frightflyList[i].gameObject.SetActive(false);
                        frightflyList[i].onUsing = false;
                        break;
                    }
                }
                break;
        }


    }

    //List<Mobs> ReturnRightList(string name)
    //{
    //    switch (name)
    //    {
    //        case "Mushroom":
    //            return mushroomList;
    //        case "Frightfly":
    //            return frightflyList;
    //    }
    //    return null;
    //}
    #region 怪物出生
    /// <summary>
    /// 產卵(同時判定有沒有再有地板的地方出生)
    /// </summary>
    public bool Spawn()
    {
        Mobs mob;
        Ray ray; //判斷怪物有沒有在正確位置的射線
        RaycastHit hitInfo; //擊中的資訊
        int MushroomNum = Random.Range(0, 10);
        if (MushroomNum < 6)
        {
            mob = GetMob("Mushroom");
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
                    //return true;
                }
                else
                { 
                    mob.onUsing = false;
                    //return false;
                }
            }
        }
        int FrightflyNum = Random.Range(0, 5);
        if (FrightflyNum < 5)
        {
            mob = GetMob("Frightfly");
            if (mob == null) return false;
            else
            {
                ray = new Ray(mob.gameObject.transform.position, Vector3.down);
                if (Physics.Raycast(ray, out hitInfo, 9999.0f, 1 << LayerMask.NameToLayer("Ground")))
                {
                    //重新賦Y值，Y值等於射線打到的點，套在怪物身上記得把+0.5f拔掉，因為pivot會在腳上
                    var mobP = mob.gameObject.transform.localPosition;
                    mobP.y = hitInfo.point.y+Random.Range(7,12);
                    mob.gameObject.transform.localPosition = mobP;
                    mob.gameObject.SetActive(true);
                    return true;
                }
                else
                {
                    mob.onUsing = false;
                    return false;
                }
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
            InvokeRepeating("Spawn", 1.0f, 3.0f);
        }
        else CancelInvoke("Spawn"); //停止InvokeRepeating的方法
    }

    #endregion
}
