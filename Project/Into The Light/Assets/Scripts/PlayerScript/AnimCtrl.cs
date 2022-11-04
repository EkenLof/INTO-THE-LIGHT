using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    [Header ("Player Anim Controller")]
    [SerializeField] static Animator anim;

    static bool isWalk = false;
    static bool isRun = false;

    static bool isLighter = false;

    string isWalkName = "isWalking";
    string isRunName = "isRunning";
    string isLighterName = "isLighterOn";

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool isWalkKeys = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        bool isRunKeys = Input.GetKey(KeyCode.LeftShift);
        bool isLighterKeys = Input.GetKeyDown("f");

        if (isWalkKeys)
        {
            isWalk = true;
            isRun = false;
            AnimationPlay();
        }
        else if (isRunKeys && isWalkKeys) // Kolla verför den ej blir OK
        {
            isRun = true;
            isWalk = false;
            AnimationPlay();
        } else
        {
            isWalk=false;
            isRun=false;
            AnimationPlay();
        }

        if (isLighterKeys && !isLighter) isLighter = true;
        else if (isLighterKeys && isLighter) isLighter = false;

    }

    void AnimationPlay()
    {
        if (isWalk) anim.SetBool(isWalkName, true);
        if (!isWalk) anim.SetBool(isWalkName, false);

        if (isRun) anim.SetBool(isRunName, true);
        if (!isRun) anim.SetBool(isRunName, false);

        if (isLighter) anim.SetBool(isLighterName, true);
        if (!isLighter) anim.SetBool(isLighterName, false);
    }
}
