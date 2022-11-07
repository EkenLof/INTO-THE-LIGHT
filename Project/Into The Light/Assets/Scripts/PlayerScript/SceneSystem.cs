using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    public string nextScene = "";
    public string prevScene = "";

    [SerializeField] bool active = false;
    [SerializeField] bool actionNext = false;
    [SerializeField] bool actionPrev = false;

    public GameObject loadingScreenUi;

    void Update()
    {
        bool sceneLoadKeys = Input.GetMouseButtonDown(0);

        if (active && actionNext && sceneLoadKeys) LoadingScene(nextScene);
        else if (active && actionPrev && sceneLoadKeys) LoadingScene(prevScene);
        else loadingScreenUi.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.tag == "Player") active = true;    
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") active = false; 
    }
    public void LoadingScene(string sceneName)
    {
        if(active) loadingScreenUi.SetActive(true);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
