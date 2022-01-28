using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Mobs
{
    [HideInInspector]
    public GameObject gameObject;
    [HideInInspector]
    public bool onUsing ; //是否在場上
    public int maxHp; //總血量
    protected int hp; //當前血量
    protected int dmg; //傷害
    protected float speed; //走路速度
    protected float mobMeleeAttackRange; //進戰攻擊距離
    protected float mobSpellAttackRange; //法術攻擊距離
    protected abstract void Ai();
}

public class  Mushroom : Mobs
{
    public Mushroom()
    {
        maxHp = 80; //當前血量
        hp = maxHp;
        dmg = 12;
        mobMeleeAttackRange = 1;
        mobSpellAttackRange = 6;
        speed = 6;
    }
    protected override void Ai()
    {
        throw new System.NotImplementedException();
    }
}
