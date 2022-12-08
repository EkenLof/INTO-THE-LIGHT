using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject menuUi;
    public GameObject pausedMenuUi;
    public GameObject optionsMenuUi;
    public GameObject saveMenuUi;

    public PlayerController player;
    public CamController cam;
    public DragMoveRig icons;

    [SerializeField] bool isIconsInScene;
    [SerializeField] bool isMainMenu = false;

    [Header("Loading")]
    AsyncOperation loadingOperation;
    public GameObject loadingScreenUi;
    public Slider progressBar;
    float progressValue;
    public Text progressText;
    int sceneIndex = 1;

    void Start()
    {
        if(!isMainMenu) menuUi.gameObject.SetActive(false);
        optionsMenuUi.gameObject.SetActive(false);
        saveMenuUi.gameObject.SetActive(false);

        if (isMainMenu) Cursor.visible = true;
        if (!isMainMenu) Cursor.lockState = CursorLockMode.Locked; //?? Låsa i mitten vid start ??

        if (!isMainMenu) player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CamController>();
        if(isIconsInScene) icons = GameObject.FindGameObjectWithTag("Player").GetComponent<DragMoveRig>();
    }

    void Update()
    {
        if (isMainMenu) Cursor.visible = true;
        if (!isMainMenu)
        {
            if (Input.GetKeyDown("escape") && !isPaused)
            {
                isPaused = true;
                OnPausedController();
            }
            else if (Input.GetKeyDown("escape") && isPaused)
            {
                isPaused = false;
                OnPausedController();
            }
        }     
    }

    public void OnPausedController()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            menuUi.gameObject.SetActive(true);
            pausedMenuUi.gameObject.SetActive(true);

            if(isIconsInScene)
            {
                icons.setCrossMouse(true);
                icons.enabled = false;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (!isPaused)
        {
            Time.timeScale = 1;
            menuUi.gameObject.SetActive(false);
            pausedMenuUi.gameObject.SetActive(false);
            optionsMenuUi.gameObject.SetActive(false);

            if (isIconsInScene) icons.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    public void OnResumeGame()
    {
        isPaused = false;
        OnPausedController();
    }

    public void OnOptionsGame()
    {
        pausedMenuUi.gameObject.SetActive(false);
        optionsMenuUi.gameObject.SetActive(true);
    }

    public void OnBackGame()
    {
        pausedMenuUi.gameObject.SetActive(true);
        optionsMenuUi.gameObject.SetActive(false);
    }

    public void OnSaveGame()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);

        isPaused = false;

        saveMenuUi.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void OnLoadGame()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        transform.position = new Vector3(x, y, z);

        isPaused = false;
    }

    public void OnContinueGame()
    {
        //Future functions save mm...
    }

    public void OnNewGame()
    {
        loadingScreenUi.SetActive(true);
        loadingOperation = SceneManager.LoadSceneAsync(sceneIndex);
        menuUi.gameObject.SetActive(false); //Test
        if (loadingOperation.isDone) loadingScreenUi.SetActive(false);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
