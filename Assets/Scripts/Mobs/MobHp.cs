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
        Object getMobHpBarPrefab = Resources.Load("Mobs/HpUI"); //讀取prefab
        canvas = GameObject.FindWithTag("GameCanvas");
        hpbar = Instantiate(getMobHpBarPrefab, canvas.transform) as GameObject; //轉實體
        hpbar.SetActive(this.gameObject.activeSelf); //顯示狀態與此物件相同
        //hpbar.transform.parent = canvas.transform; //設定爸爸回去給canvas
        hpbar.gameObject.name = "MushroomHp";
    }

    void Update()
    {
        PHFollowEnemy();
        GetHurt();
    }
    void PHFollowEnemy()
    {
        //把物體座標轉換為螢幕座標，修改偏移量
        //hpbarWorldPosition = Camera.main.WorldToScreenPoint(this.transform.position)+new Vector3(0,50,0);
        //hpbar.transform.position = hpbarWorldPosition;

        hpbarWorldPosition = Camera.main.ScreenToWorldPoint(hpbar.transform.position); //將血條轉成3D位置
        hpbarWorldPosition = new Vector3(this.transform.position.x, this.transform.position.y + 3.0f, this.transform.position.z);//血條3D位置=怪物+偏量 
        hpbar.transform.localScale = new Vector3(2, 2, 2); //縮放是1
        hpbar.transform.forward = Camera.main.transform.forward; //永遠面向攝影機
        hpbar.transform.position = hpbarWorldPosition;
    }

    void GetHurt()
    {
        //按下H鈕扣血
        if (Input.GetKeyDown(KeyCode.H))
        {
            //接受傷害
            currentHealth -= 10;
        }

        //將綠色血條同步到當前血量長度
        hpBar.sizeDelta = new Vector2(currentHealth, hpBar.sizeDelta.y);

        //呈現傷害量
        if (hurt.sizeDelta.x > hpBar.sizeDelta.x)
        {
            //讓傷害量(紅色血條)逐漸追上當前血量
            hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 10;
        }
    }
}
