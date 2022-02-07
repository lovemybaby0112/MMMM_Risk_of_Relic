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
    protected float meleeDmg; //�ˮ`
    protected float spellDmg; //�ˮ`
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
        meleeDmg = 12;
        spellDmg = 3;//�@��T�w
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
        maxHp = 35; //��e��q
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
        maxHp = 55; //��e��q
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
        maxHp = 70; //��e��q
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
