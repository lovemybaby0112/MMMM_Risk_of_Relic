using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Mobs
{
    [HideInInspector]
    public GameObject gameObject;
    [HideInInspector]
    public bool onUsing ; //�O�_�b���W
    public int maxHp; //�`��q
    public int hp; //��e��q
    protected float dmg; //�ˮ`
    protected float speed; //�����t��
    protected float mobMeleeAttackRange; //�i�ԧ����Z��
    protected float mobSpellAttackRange; //�k�N�����Z��
    protected float critAttack; //�z���ˮ`
    protected abstract void Ai();
}

public class  Mushroom : Mobs
{
    public Mushroom()
    {
        maxHp = 80; //��e��q
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
        maxHp = 35; //��e��q
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
