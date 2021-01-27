using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    public bool destroyThisGameObjectOnLoad = true;
    public int indexOfMainGameScene = 3;
    public int indexOfSecondGameScene = 4;

    private void Awake()
    {
        if (!destroyThisGameObjectOnLoad)
            DontDestroyOnLoad(this);
    }

    private void Update()
    {   
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex == indexOfMainGameScene)
        {
            Destroy(gameObject);
        }
        if(SceneManager.GetActiveScene().buildIndex == indexOfSecondGameScene)
        {
            Destroy(gameObject);
        }
    }
}   