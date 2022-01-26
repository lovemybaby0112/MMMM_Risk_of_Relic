using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHp : MonoBehaviour
{
    Vector3 hpbarWorldPosition;
    GameObject hpbar;
    GameObject canvas;
    string mobName;
    Mobs mobData;
    private int maxHealth;
    private int currentHealth;
    public RectTransform hpUI;
    private RectTransform hpBar, hurt;
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
        hpBar = hpUI.GetChild(2).GetComponent<RectTransform>();
        hurt = hpUI.GetChild(1).GetComponent<RectTransform>();
        maxHealth = mobData.maxHp;
        currentHealth = maxHealth;
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
        GetHurt();
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

    void GetHurt()
    {
        //���UH�s����
        if (Input.GetKeyDown(KeyCode.H))
        {
            //�����ˮ`
            currentHealth -= 10;
        }

        //�N������P�B���e��q����
        hpBar.sizeDelta = new Vector2(currentHealth, hpBar.sizeDelta.y);

        //�e�{�ˮ`�q
        if (hurt.sizeDelta.x > hpBar.sizeDelta.x)
        {
            //���ˮ`�q(������)�v���l�W��e��q
            hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 10;
        }
    }
}
