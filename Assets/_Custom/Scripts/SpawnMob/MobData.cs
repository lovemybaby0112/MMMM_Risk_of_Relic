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

    protected virtual void Ai()
    {

    }
}
