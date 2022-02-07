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
    public int hp; //當前血量
    protected float dmg; //傷害
    protected float speed; //走路速度
    protected float mobMeleeAttackRange; //進戰攻擊距離
    protected float mobSpellAttackRange; //法術攻擊距離
    protected float critAttack; //爆擊傷害
    protected abstract void Ai();
}

public class  Mushroom : Mobs
{
    public Mushroom()
    {
        maxHp = 80; //當前血量
        hp = maxHp;
        dmg = 12;
        critAttack = 24;
        mobMeleeAttackRange = 3;
        mobSpellAttackRange = 10;
        speed = 10;
    }
    protected override void Ai()
    {
        throw new System.NotImplementedException();
    }
}

public class Frightfly : Mobs
{
    public Frightfly()
    {
        maxHp = 35; //當前血量
        hp = maxHp;
        dmg = 3.5f;
        critAttack = 5;
        mobMeleeAttackRange = 3;
        mobSpellAttackRange = 10;
        speed = 10;
    }
    protected override void Ai()
    {
        throw new System.NotImplementedException();
    }
}
