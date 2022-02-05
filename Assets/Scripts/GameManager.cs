using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerIns;
    private List<Obstacle> obstacles;
    private void Awake()
    {
        LoadPlayer();
        gameManagerIns = this;
    }
    void Start()
    {
        #region 創建怪物物件
        string mobname = "Mushroom";
        for (int i = 0; i < 10; i++)
        {
            MobManager.Instance().CreateMobs(mobname);
        }
        #endregion
        #region 抓取障礙物
        obstacles = new List<Obstacle>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");
        if (gos != null || gos.Length > 0)
        {
            foreach (GameObject go in gos)
            {
                obstacles.Add(go.GetComponent<Obstacle>());
            }
        }
        #endregion
    }
    void LoadPlayer()
    {
        Transform PlayerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
        Instantiate(Resources.Load("Players/PlayerArcher"), PlayerSpawnPoint.position, PlayerSpawnPoint.rotation);
    }
    public List<Obstacle> GetObstacles()
    {
        return obstacles;
    }

}
