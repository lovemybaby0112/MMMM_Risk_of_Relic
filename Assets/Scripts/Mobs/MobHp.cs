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
    private float maxHealth; //最大血量
    public float currentHealth; //當前血量
    private float hpBarRectWidth; //血條UI的長度
    private RectTransform hpUIRect;
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
        Object getMobHpBarPrefab = Resources.Load("Mobs/HpUI"); //讀取prefab
        canvas = GameObject.FindWithTag("GameCanvas");
        hpUI = Instantiate(getMobHpBarPrefab, canvas.transform) as GameObject; //轉實體
        hpUI.SetActive(this.gameObject.activeSelf); //顯示狀態與此物件相同
        hpUI.gameObject.name = "MushroomHp";
        
        //~~~~~~~~扣血相關設定~~~~~~~~~~~
        maxHealth = mobData.maxHp; //設定怪物最大血量
        currentHealth = maxHealth; //怪物當前血量
        hpUIRect = hpUI.GetComponent<RectTransform>(); //抓取血條UI的RectTransform
        hpBar = hpUIRect.GetChild(2).GetComponent<RectTransform>(); //getUI的孩子去做扣血效果
        hurt = hpUIRect.GetChild(1).GetComponent<RectTransform>();
        hpBarRectWidth = hpBar.rect.width; //得到血條本身的寬度
    }


    void Update()
    {
        if(gameObject.activeSelf ==true)
        {
            hpUI.SetActive(true);
        }
        PHFollowEnemy();
    }
    void PHFollowEnemy()
    {
        //把物體座標轉換為螢幕座標，修改偏移量
        //hpbarWorldPosition = Camera.main.WorldToScreenPoint(this.transform.position)+new Vector3(0,50,0);
        //hpbar.transform.position = hpbarWorldPosition;
        hpbarWorldPosition = Camera.main.ScreenToWorldPoint(hpUI.transform.position); //將血條轉成3D位置
        hpbarWorldPosition = new Vector3(this.transform.position.x, this.transform.position.y + 3.0f, this.transform.position.z);//血條3D位置=怪物+偏量 
        hpUI.transform.localScale = new Vector3(2, 2, 2); //縮放是1
        hpUI.transform.forward = Camera.main.transform.forward; //永遠面向攝影機
        hpUI.transform.position = hpbarWorldPosition;
    }

    void GetHurt()
    {
        //接受傷害
        currentHealth -= 10;

        ////按下H鈕扣血
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    
        //}

        float healthPercent = currentHealth / maxHealth * hpBarRectWidth;

        //將綠色血條同步到當前血量長度
        hpBar.sizeDelta = new Vector2(healthPercent, hpBar.sizeDelta.y);

        //呈現傷害量
        if (hurt.sizeDelta.x > hpBar.sizeDelta.x)
        {
            //讓傷害量(紅色血條)逐漸追上當前血量
            hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 8;
        }

        //當怪物血量為0時死亡，重製
        if(currentHealth <= 0)
        {
            hpUI.SetActive(false); //隱藏血條
            StartCoroutine(ResetHpAndMob());
        }
    }

    /// <summary>
    /// 重設血量與怪物狀態
    /// </summary>
    public IEnumerator ResetHpAndMob()
    {
        yield return new WaitForSeconds(5.0f);
        //回去重設怪物狀態
        MobManager.Instance().ResetMob(this.gameObject);
        currentHealth = maxHealth; //血量恢復最大血量
        //UI回到正常長度
        hpBar.sizeDelta = new Vector2(hpBarRectWidth, hpBar.sizeDelta.y);
        hurt.sizeDelta = new Vector2(hpBarRectWidth, hpBar.sizeDelta.y);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "attack")
        {
            GetHurt();
        }
        Destroy(other);
    }
}
