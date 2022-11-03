using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    [Header ("Player Anim Controller")]
    [SerializeField] static Animator anim;

    static bool isWalk = false;
    static bool isRun = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool isWalkKeys = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        bool isRunKeys = Input.GetKey(KeyCode.LeftShift);

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

    }

    void AnimationPlay()
    {
        if (isWalk)
            anim.SetBool("isWalking", true);
        if (isRun)
            anim.SetBool("isRunning", true);

        if (!isWalk)
            anim.SetBool("isWalking", false);
        if (!isRun)
            anim.SetBool("isRunning", false);
    }
}
