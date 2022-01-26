using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHp : MonoBehaviour
{
    Vector3 hpbarWorldPosition;
    GameObject hpbar;
    GameObject canvas;
    void Start()
    {
        Object getMobHpBarPrefab = Resources.Load("Mobs/HpUI"); //Ū��prefab
        canvas = GameObject.FindWithTag("GameCanvas");
        hpbar = Instantiate(getMobHpBarPrefab, canvas.transform) as GameObject; //�����
        hpbar.SetActive(this.gameObject.activeSelf); //��ܪ��A�P������ۦP
        //hpbar.transform.parent = canvas.transform; //�]�w�����^�h��canvas
        hpbar.gameObject.name = "MushroomHp";
    }

    void Update()
    {
        PHFollowEnemy();
    }
    void PHFollowEnemy()
    {
        //�⪫��y���ഫ���ù��y�СA�קﰾ���q
        //hpbarWorldPosition = Camera.main.WorldToScreenPoint(this.transform.position)+new Vector3(0,50,0);
        //hpbar.transform.position = hpbarWorldPosition;

        hpbarWorldPosition = Camera.main.ScreenToWorldPoint(hpbar.transform.position); //�N����ন3D��m
        hpbarWorldPosition = new Vector3(this.transform.position.x, this.transform.position.y + 3.0f, this.transform.position.z);//���3D��m=�Ǫ�+���q 
        hpbar.transform.localScale = new Vector3(2, 2, 2); //�Y��O1
        hpbar.transform.forward = Camera.main.transform.forward; //�û����V��v��
        hpbar.transform.position = hpbarWorldPosition;

    }
}
