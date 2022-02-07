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
    protected float meleeDmg; //傷害
    protected float spellDmg; //傷害
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
        meleeDmg = 12;
        spellDmg = 3;//一秒三滴
        critAttack = 2; //200%
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
        meleeDmg = 11.5f;
        spellDmg = 22.5f;
        critAttack = 1.5f;//150%
        mobMeleeAttackRange = 3;
        mobSpellAttackRange = 20;
        speed = 10;
    }
    protected override void Ai()
    {
        throw new System.NotImplementedException();
    }
}
public class FIRE_PeaShooter : Mobs
{
    public FIRE_PeaShooter()
    {
        maxHp = 55; //當前血量
        hp = maxHp;
        meleeDmg = 6.0f;
        spellDmg = 13.5f;
        critAttack = 1.5f;//150%
        mobMeleeAttackRange = 3;
        mobSpellAttackRange = 17;
        speed = 15;
    }
    protected override void Ai()
    {
        throw new System.NotImplementedException();
    }
}
public class ICE_PeaShooter : Mobs
{
    public ICE_PeaShooter()
    {
        maxHp = 70; //當前血量
        hp = maxHp;
        meleeDmg = 6.0f;
        spellDmg = 8.5f;
        critAttack = 2f;//200%
        mobMeleeAttackRange = 3;
        mobSpellAttackRange = 15;
        speed = 15;
    }
    protected override void Ai()
    {
        throw new System.NotImplementedException();
    }
}
