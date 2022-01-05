using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs
{
    [HideInInspector]
    public GameObject gameObject;
    [HideInInspector]
    public bool onUsing = false; //�O�_�b���W
    protected int maxHp; //�`��q
    [HideInInspector]
    public int hp; //��e��q
    [HideInInspector]
    public int dmg; //�ˮ`
    protected int speed; //�����t��

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
