using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_Info : MonoBehaviour
{
    public GameObject LoadingMask;
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
        SceneManager.LoadScene("Menu");
    }

    public void Loading()
    {     
        //SceneManager.LoadSceneAsync("Main");
        LoadingMask.SetActive(true);
    }
}
