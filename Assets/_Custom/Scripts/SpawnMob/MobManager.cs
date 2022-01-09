using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    
    private static MobManager MobManager_Ins; //singleton��Ҥ�
    public static MobManager Instance() { return MobManager_Ins; }

    public GameObject mob_FatherGameObject;//��mob��������
    List<Mobs> mobsList;
    int count; //�Ǫ��}�C�`����
    GameObject[] threeMobs; //�˨C�����ͤT���Ǫ��}�C

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
        threeMobs = new GameObject[3]; //���~���@�����ͤT��
    }

    /// <summary>
    /// �ЫةǪ�
    /// </summary>
    /// <param name="mobName">Prefab���W��</param>
    public void CreateMobs(string mobName)
    {
        GameObject mobGameObject = null;
        Object getMobPrefab = Resources.Load("Mobs/mob"); //��������W�ݩ�o�ӦW�٪�prefab    
        mobGameObject = Instantiate(getMobPrefab) as GameObject;  //�૬
        mobGameObject.SetActive(false);
        mobGameObject.transform.parent = mob_FatherGameObject.transform; //��J������
        //�N�L��i�Ǫ�List�̭�
        Mobs mob = new Mobs();
        mob.gameObject = mobGameObject;
        mob.onUsing = false;
        mobsList.Add(mob);
    }

    /// <summary>
    /// �o�쥼�A�ϥΤ����Ǫ�����A�]�w���m�A�ç��]�w���ϥΤ�
    /// </summary>
    /// <returns></returns>
    public GameObject[] GetMob()
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
                    mob = mobsList[i].gameObject;
                    break;
                }
            }
            //if (mob == null) return null;
            threeMobs[n] = mob;
        }
        threeMobs[0].transform.position = new Vector3(Random.Range(minX, maxX), 50.0f, Random.Range(minZ, maxZ));//�Ĥ@���Ǧbbox�d���H���X��
        float mob1_X = threeMobs[0].transform.position.x;
        float mob1_Z = threeMobs[0].transform.position.z;
        //�᭱�Ⱖ�ǥͦb�Ĥ@���Ǯ���
        threeMobs[1].transform.position = new Vector3(Random.Range(mob1_X + 1, mob1_X + 5), 50.0f, Random.Range(mob1_Z + 1, mob1_Z + 5));
        threeMobs[2].transform.position = new Vector3(Random.Range(mob1_X - 1, mob1_X - 5), 50.0f, Random.Range(mob1_Z - 1, mob1_Z - 5));          
        if (threeMobs == null) return null;
        return threeMobs;
    }

    /// <summary>
    /// �⤣�b���T��m���Ǫ�OnUse�]��false
    /// </summary>
    /// <param name="gameObject"></param>
    public void SetMobOnUsingFalse(GameObject gameObject)
    {
        for (int i = 0; i < count; i++)
        {
            if (mobsList[i].gameObject == gameObject) mobsList[i].onUsing = false;
        }
    }

    #region �Ǫ��X��
    GameObject[] mob;

    /// <summary>
    /// ���Z(�P�ɧP�w���S���A���a�O���a��X��)
    /// </summary>
    public void Spawn()
    {
        Ray ray; //�P�_�Ǫ����S���b���T��m���g�u
        RaycastHit hitInfo; //��������T
        int num = Random.Range(0, 10);
        Debug.Log(num);
        if (num < 6)
        {
            mob = GetMob();
            for (int i = 0; i < mob.Length; i++)
            {
                ray = new Ray(mob[i].transform.position, Vector3.down);
                if (Physics.Raycast(ray, out hitInfo, 9999.0f, 1 << LayerMask.NameToLayer("Terrain")))
                {
                    //���s��Y�ȡAY�ȵ���g�u���쪺�I�A�M�b�Ǫ����W�O�o��+0.5f�ޱ��A�]��pivot�|�b�}�W
                    var mobP = mob[i].transform.localPosition;
                    mobP.y = hitInfo.point.y + 0.5f;
                    mob[i].transform.localPosition = mobP;
                    //Debug.DrawRay(ray.origin, Vector3.down, Color.blue, 3.0f);
                    mob[i].SetActive(true);                  
                }
                else SetMobOnUsingFalse(mob[i]);
            }
        }
    }

    /// <summary>
    /// ���沣�Z�A�X���}�l�A�C�X�����@��
    /// </summary>
    /// <param name="spawn"></param>
    public void DoSpawn(bool spawn)
    {
        if (spawn)
        {
            InvokeRepeating("Spawn", 3.0f, 1.0f);
        }
        else CancelInvoke("Spawn"); //����InvokeRepeating����k
    }

    #endregion
}
