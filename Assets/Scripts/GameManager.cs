using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        LoadPlayer();
    }
    void LoadPlayer()
    {
        Transform PlayerSpwanPoint = GameObject.Find("PlayerSpwanPoint").transform;
        Instantiate(Resources.Load("Players/PlayerArcher"), PlayerSpwanPoint.position, PlayerSpwanPoint.rotation);
    }
    
}
