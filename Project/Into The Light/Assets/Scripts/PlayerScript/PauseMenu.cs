using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;

    public GameObject menuUi;
    public GameObject pausedMenuUi;
    public GameObject optionsMenuUi;
    public GameObject saveMenuUi;

    //public GameObject effect; // Fixa 2022

    //public Volume allEffects;
    //DepthOfField onDof;

    public PlayerController player;
    public CamController cam;
    public DragMoveRig icons;

    void Start()
    {
        menuUi.gameObject.SetActive(false);
        optionsMenuUi.gameObject.SetActive(false);
        saveMenuUi.gameObject.SetActive(false);
        //effect.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked; //?? Låsa i mitten vid start ??

        isPaused = false;

        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CamController>();
        icons = GameObject.FindGameObjectWithTag("Player").GetComponent<DragMoveRig>();
        //allEffects = GameObject.FindWithTag("AllEffectCtrl").GetComponent<Volume>();

        //allEffects.profile.TryGet<DepthOfField>(out onDof);

        //onDof.focusMode
    }

    void Update()
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

    public void OnPausedController()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            menuUi.gameObject.SetActive(true);
            pausedMenuUi.gameObject.SetActive(true);

            //effect.gameObject.SetActive(true);

            //2022
            icons.setCrossMouse();
            icons.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (!isPaused)
        {
            Time.timeScale = 1;
            menuUi.gameObject.SetActive(false);
            pausedMenuUi.gameObject.SetActive(false);
            optionsMenuUi.gameObject.SetActive(false);

            //effect.gameObject.SetActive(false);

            //2022
            icons.enabled = true;
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

    public void OnExitGame()
    {
        Application.Quit();
    }
}
