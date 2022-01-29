using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    
    private static MobManager MobManager_Ins; //singleton��Ҥ�
    public static MobManager Instance() { return MobManager_Ins; }
    public GameObject mob_FatherGameObject;//��mob��������

    List<Mobs> mobsList;
    int count; //�Ǫ��}�C�`����
    //Mobs[] threeMobs; //�˨C�����ͤT���Ǫ��}�C

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
        mobsList = new List<Mobs>();
        //threeMobs = new Mobs[3]; //���~���@�����ͤT��
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
                mobsList.Add(mushroom);
                break;
        }
    }

    /// <summary>
    /// �o�쥼�A�ϥΤ����Ǫ�����A�]�w���m�A�ç��]�w���ϥΤ�
    /// </summary>
    /// <returns></returns>
    public List<Mobs> GetMob()
    {
        List<Mobs> threeMobs = new List<Mobs>();
        count = mobsList.Count;
        for (int n = 0; n < 3; n++)
        {
            for (int i = 0; i < count; i++)
            {
                if (mobsList[i].onUsing == false)
                {
                    mobsList[i].onUsing = true;
                    threeMobs.Add(mobsList[i]);
                    break;
                }
            }
        }
        if (threeMobs[0] != null)
        {
            threeMobs[0].gameObject.transform.position = new Vector3(Random.Range(minX, maxX), 999.0f, Random.Range(minZ, maxZ));//�Ĥ@���Ǧbbox�d���H���X��
            float mob1_X = threeMobs[0].gameObject.transform.position.x;
            float mob1_Z = threeMobs[0].gameObject.transform.position.z;
            //�᭱�Ⱖ�ǥͦb�Ĥ@���Ǯ���
            if (threeMobs[1] != null) 
                threeMobs[1].gameObject.transform.position = new Vector3(Random.Range(mob1_X + 1, mob1_X + 5), 999.0f, Random.Range(mob1_Z + 1, mob1_Z + 5));
            else return null;
            if (threeMobs[2] != null)
                threeMobs[2].gameObject.transform.position = new Vector3(Random.Range(mob1_X - 1, mob1_X - 5), 999.0f, Random.Range(mob1_Z - 1, mob1_Z - 5));
            else return null;
            return threeMobs;
        }
        else return null;
    }

    /// <summary>
    /// �Ǫ����`���m
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
                Debug.Log(mobsList[i].onUsing);
                break;
            }
        }
    }
    #region �Ǫ��X��
    /// <summary>
    /// ���Z(�P�ɧP�w���S���A���a�O���a��X��)
    /// </summary>
    public bool Spawn()
    {
        List<Mobs> mob;
        Ray ray; //�P�_�Ǫ����S���b���T��m���g�u
        RaycastHit hitInfo; //��������T
        int num = Random.Range(0, 10);
        Debug.Log(num);
        if (num < 6)
        {
            mob = GetMob();
            if (mob == null) return false;
            else
            {

                for (int i = 0; i < mob.Count; i++)
                {                  
                    ray = new Ray(mob[i].gameObject.transform.position, Vector3.down);
                    if (Physics.Raycast(ray, out hitInfo, 9999.0f, 1 << LayerMask.NameToLayer("Ground")))
                    {
                        //���s��Y�ȡAY�ȵ���g�u���쪺�I�A�M�b�Ǫ����W�O�o��+0.5f�ޱ��A�]��pivot�|�b�}�W
                        var mobP = mob[i].gameObject.transform.localPosition;
                        mobP.y = hitInfo.point.y;
                        mob[i].gameObject.transform.localPosition = mobP;
                        //Debug.Log("SetActive");
                        mob[i].gameObject.SetActive(true);
                        //Debug.Log(mob[i].onUsing);
                    }
                    else mob[i].onUsing = false;
                }
                return true;
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
        Debug.Log("���");
        if (spawn)
        {
            if (Spawn())
            {
                InvokeRepeating("Spawn", 1.0f, 3.0f);
            }
        }
        else CancelInvoke("Spawn"); //����InvokeRepeating����k
    }

    #endregion
}
