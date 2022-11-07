using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
//using UnityEngine.UIElements;

public class SceneSystem : MonoBehaviour
{
    public string nextScene = "";
    public string prevScene = "";

    [SerializeField] bool active = false;
    [SerializeField] bool actionNext = false;
    [SerializeField] bool actionPrev = false;

    [Header ("Loading")]
    AsyncOperation loadingOperation;
    public GameObject loadingScreenUi;
    public Slider progressBar;
    float progressValue;
    public Text progressText;

    void Update()
    {
        bool sceneLoadKeys = Input.GetMouseButtonDown(0);

        //if (active && actionNext && sceneLoadKeys) LoadingScene(nextScene);
        //else if (active && actionPrev && sceneLoadKeys) LoadingScene(prevScene);
    }

    void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.tag == "Player") active = true;    
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") active = false; 
    }
    public void LoadingScene(int sceneName)
    {
        //loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        if (loadingOperation.isDone) loadingScreenUi.SetActive(false);

        //if (active) loadingScreenUi.SetActive(true);
        //////////StartCoroutine(LoadAsynchronously());
        //SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerable LoadAsynchronously(int sceneName)
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreenUi.SetActive(true);

        while (!loadingOperation.isDone)
        {
            

            yield return null;
        }
    }
}
