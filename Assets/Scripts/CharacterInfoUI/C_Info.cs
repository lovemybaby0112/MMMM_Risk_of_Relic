using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_Info : MonoBehaviour
{
    public GameObject loadingMask;
    public GameObject uiMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSceneToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Loading()
    {
        loadingMask.SetActive(true);
        uiMask.SetActive(false);
        SceneManager.LoadSceneAsync("GameScene"); //切到遊戲畫面

    }
}
