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
    protected int hp; //��e��q
    protected int dmg; //�ˮ`
    protected float speed; //�����t��
    protected float mobMeleeAttackRange; //�i�ԧ����Z��
    protected float mobSpellAttackRange; //�k�N�����Z��
    protected abstract void Ai();
}

public class  Mushroom : Mobs
{
    public Mushroom()
    {
        maxHp = 80; //��e��q
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
