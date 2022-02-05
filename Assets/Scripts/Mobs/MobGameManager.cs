using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGameManager : MonoBehaviour
{
    public static GameManager gameManagerIns;
    private List<Obstacle> obstacles;


    private void Awake()
    {
        //gameManagerIns = this;
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
        obstacles = new List<Obstacle>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");
        if (gos != null || gos.Length > 0)
        {
            //Debug.Log(gos[0]);
            foreach (GameObject go in gos)
            {
                //Debug.Log(go);
                obstacles.Add(go.GetComponent<Obstacle>());
                Debug.Log(obstacles[0]);
            }
        }
    }

    void Update()
    {

    }
    public List<Obstacle> GetObstacles()
    {
        return obstacles;
    }
}
