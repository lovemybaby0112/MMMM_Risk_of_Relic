using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public RectTransform playerIcon;
    public RectTransform miniMapEnd;
    public Transform defindMapSizeCenter;
    public Transform defindMapSizeEnd;
    public Transform playerInGame;

    private Vector3 normalized, mapped;

    private void Awake()
    {
        Instantiate(Resources.Load("UI/MinimapUI"));
        GetMiniMapGameObjects();
    }
    private void Start()
    {
        playerInGame = GameObject.Find("Archer").transform;
    }
    private void Update()
    {
        PlayerIconInMap();
    }

    private static Vector3 MapDivide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    private static Vector3 MapMultiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    void PlayerIconInMap()
    {
        normalized = MapDivide(defindMapSizeCenter.InverseTransformPoint(playerInGame.position), defindMapSizeEnd.position - defindMapSizeCenter.position);
        normalized.y = normalized.z;
        mapped = MapMultiply(normalized, miniMapEnd.localPosition);
        mapped.z = 0;
        playerIcon.localPosition = mapped;
        playerIcon.localEulerAngles = new Vector3(0.0f, 0.0f, -playerInGame.eulerAngles.y);
    }
    void GetMiniMapGameObjects()
    {
        playerIcon = GameObject.Find("PlayerIcon").transform as RectTransform;
        miniMapEnd = GameObject.Find("MiniMapEnd").transform as RectTransform;
        defindMapSizeCenter = GameObject.Find("MapSizeCenter").transform;
        defindMapSizeEnd = GameObject.Find("MapSizeEnd").transform;
    }
}
