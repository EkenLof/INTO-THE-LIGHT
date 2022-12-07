using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
//using UnityEngine.UIElements;

public class SceneSystem : MonoBehaviour
{
    [Header("Asign")]
    public int nextScene;
    public int prevScene;

    public string objEntering;
    [SerializeField] float timerToSceneFade = 50f;

    [SerializeField] bool active = false;
    [SerializeField] bool actionNext = false;
    [SerializeField] bool actionPrev = false;
    [SerializeField] bool isTimeTriggerd = false;

    [Header ("Loading")]
    AsyncOperation loadingOperation;
    public GameObject loadingScreenUi;
    public Slider progressBar;
    float progressValue;
    public Text progressText;

    private void Start()
    {
        loadingScreenUi.SetActive(false);
    }

    void Update()
    {
        bool sceneLoadKeys = Input.GetMouseButtonDown(0);

        timerToSceneFade -= Time.deltaTime;

        if (isTimeTriggerd)
        {
            if (timerToSceneFade <= 0f && actionNext)
            {
                Debug.Log("Car enterd");
                LoadingScene(nextScene);
            }
        }
        if (!isTimeTriggerd)
        {
            if (active && actionNext && sceneLoadKeys) LoadingScene(nextScene);
            else if (active && actionPrev && sceneLoadKeys) LoadingScene(prevScene);
        }
    }

    void OnTriggerEnter(Collider other)
    {     
        if (other.CompareTag(objEntering)) 
        {
            Debug.Log("Enter");
            active = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == objEntering) 
        {
            Debug.Log("Exit");
            active = false;
        }
    }
    public void LoadingScene(int sceneName)
    {
        loadingScreenUi.SetActive(true);
        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
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
