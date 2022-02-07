using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MobManager : MonoBehaviour
{

    private static MobManager MobManager_Ins; //singleton��Ҥ�
    public static MobManager Instance() { return MobManager_Ins; }
    public GameObject mob_FatherGameObject;//��mob��������

    List<Mobs> mushroomList;
    List<Mobs> frightflyList;
    int count; //�Ǫ��}�C�`����

    //�Ǫ��ͦ���m�y�Ъ��j�p�ƭ�
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
    /// �ЫةǪ�
    /// </summary>
    /// <param name="mobName">Prefab���W��</param>
    public void CreateMobs(string mobName)
    {
        GameObject mobGameObject = null;

        Object getMobPrefab = Resources.Load($"Mobs/{mobName}"); //��������W�ݩ�o�ӦW�٪�prefab
        mobGameObject = Instantiate(getMobPrefab) as GameObject;  //�૬
        mobGameObject.SetActive(false);
        mobGameObject.transform.parent = mob_FatherGameObject.transform; //��J������
        //�N�L��i�Ǫ�List�̭�
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
    /// �o�쥼�A�ϥΤ����Ǫ�����A�]�w���m�A�ç��]�w���ϥΤ�
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
    /// �Ǫ����`���m
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
    #region �Ǫ��X��
    /// <summary>
    /// ���Z(�P�ɧP�w���S���A���a�O���a��X��)
    /// </summary>
    public bool Spawn()
    {
        Mobs mob;
        Ray ray; //�P�_�Ǫ����S���b���T��m���g�u
        RaycastHit hitInfo; //��������T
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
                    //���s��Y�ȡAY�ȵ���g�u���쪺�I�A�M�b�Ǫ����W�O�o��+0.5f�ޱ��A�]��pivot�|�b�}�W
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
                    //���s��Y�ȡAY�ȵ���g�u���쪺�I�A�M�b�Ǫ����W�O�o��+0.5f�ޱ��A�]��pivot�|�b�}�W
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
    /// ���沣�Z�A�X���}�l�A�C�X�����@��
    /// </summary>
    /// <param name="spawn"></param>
    public void DoSpawn(bool spawn)
    {
        if (spawn)
        {
            InvokeRepeating("Spawn", 1.0f, 3.0f);
        }
        else CancelInvoke("Spawn"); //����InvokeRepeating����k
    }

    #endregion
}
