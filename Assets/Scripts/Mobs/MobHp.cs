using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHp : MonoBehaviour
{
    Vector3 hpbarWorldPosition;
    GameObject hpUI;
    GameObject canvas;
    string mobName;
    Mobs mobData;
    private float maxHealth; //�̤j��q
    private float currentHealth; //��e��q
    private float hpBarRectWidth; //���UI������
    private RectTransform hpUIRect;
    private RectTransform hpBar, hurt;
    MobAI mobAI;
    private void Awake()
    {
        mobName = this.gameObject.name;
        switch (mobName)
        {       
            case "Mushroom(Clone)":
                mobData = new Mushroom();
                break;
        }     
    }
    void Start()
    {
        Object getMobHpBarPrefab = Resources.Load("Mobs/HpUI"); //Ū��prefab
        canvas = GameObject.FindWithTag("GameCanvas");
        hpUI = Instantiate(getMobHpBarPrefab, canvas.transform) as GameObject; //�����
        hpUI.SetActive(this.gameObject.activeSelf); //��ܪ��A�P������ۦP
        hpUI.gameObject.name = "MushroomHp";
        
        //~~~~~~~~��������]�w~~~~~~~~~~~
        maxHealth = mobData.maxHp; //�]�w�Ǫ��̤j��q
        currentHealth = maxHealth; //�Ǫ���e��q
        hpUIRect = hpUI.GetComponent<RectTransform>(); //������UI��RectTransform
        hpBar = hpUIRect.GetChild(2).GetComponent<RectTransform>(); //getUI���Ĥl�h������ĪG
        hurt = hpUIRect.GetChild(1).GetComponent<RectTransform>();
        hpBarRectWidth = hpBar.rect.width; //�o�����������e��
    }

    void Update()
    {
        if(gameObject.active ==true)
        {
            hpUI.SetActive(true);
        }
        PHFollowEnemy();
        GetHurt();
    }
    void PHFollowEnemy()
    {
        //�⪫��y���ഫ���ù��y�СA�קﰾ���q
        //hpbarWorldPosition = Camera.main.WorldToScreenPoint(this.transform.position)+new Vector3(0,50,0);
        //hpbar.transform.position = hpbarWorldPosition;
        hpbarWorldPosition = Camera.main.ScreenToWorldPoint(hpUI.transform.position); //�N����ন3D��m
        hpbarWorldPosition = new Vector3(this.transform.position.x, this.transform.position.y + 3.0f, this.transform.position.z);//���3D��m=�Ǫ�+���q 
        hpUI.transform.localScale = new Vector3(2, 2, 2); //�Y��O1
        hpUI.transform.forward = Camera.main.transform.forward; //�û����V��v��
        hpUI.transform.position = hpbarWorldPosition;
    }

    void GetHurt()
    {

        ////���UH�s����
        if (Input.GetKeyDown(KeyCode.H))
        {
            //�����ˮ`
            currentHealth -= 10;
        }

        float healthPercent = currentHealth / maxHealth * hpBarRectWidth;

        //�N������P�B���e��q����
        hpBar.sizeDelta = new Vector2(healthPercent, hpBar.sizeDelta.y);

        //�e�{�ˮ`�q
        if (hurt.sizeDelta.x > hpBar.sizeDelta.x)
        {
            //���ˮ`�q(������)�v���l�W��e��q
            hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 5;
        }

        //��Ǫ���q��0�ɦ��`�A���s
        if(healthPercent <= 0)
        {
            ResetHpAndMob();
        }
    }

    private void ResetHpAndMob()
    {
        hpUI.SetActive(false);
        MobManager.Instance().ResetMob(this.gameObject);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag =="attack")
    //    {
    //        GetHurt();
    //    }
    //    Destroy(other);
    //}
}
