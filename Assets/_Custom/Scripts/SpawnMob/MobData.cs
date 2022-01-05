using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs
{
    [HideInInspector]
    public GameObject gameObject;
    [HideInInspector]
    public bool onUsing = false; //是否在場上
    protected int maxHp; //總血量
    [HideInInspector]
    public int hp; //當前血量
    [HideInInspector]
    public int dmg; //傷害
    protected int speed; //走路速度

    public void GetHurt(int playerDmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
        }
    }
    protected virtual void Ai()
    {

    }
}
