using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerIns;
    private List<GameObject> obstacles;

    private void Awake()
    {
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
        obstacles = new List<GameObject>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");
        if (gos != null || gos.Length > 0)
        {
            //Debug.Log(gos.Length);
            foreach (GameObject go in gos)
            {
                obstacles.Add(go);
            }
        }
    }

    void Update()
    {
        
    }
    public List<GameObject> GetObstacles()
    {
        return obstacles;
    }
}
