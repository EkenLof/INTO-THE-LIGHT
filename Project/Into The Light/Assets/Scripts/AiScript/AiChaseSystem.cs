﻿using System;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(AI1Character))]

public class AiChaseSystem : MonoBehaviour
{
    //2022

    [Header("AI Anim Controller")]
    static Animator animatorAi;

    [SerializeField] bool isMoving = false;
    [SerializeField] bool isTooNear = false;
    [SerializeField] bool isSneakPeeking = false;

    string isMoveName = "isWalk";
    string isTooNearName = "isToNear";
    string isSneakPeekName = "isSneakPeek";

    float distance;
    [SerializeField] float interactionRange = 3f;

    //2022

    public float timeChase = 15.0f;
    public float maxTimeChase = 15.0f;

    public bool exitTimeTrigger;
    public bool resetTime;

    AudioSource chaseAudio;

    // 2020 test start
    public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
    public AI1Character character { get; private set; } // the character we are controlling
    public Transform target; // target to aim for

    public bool chase;
    // 2020 test end

    void Start()
    {
        //2022
        animatorAi = GetComponent<Animator>();

        exitTimeTrigger = false;
        resetTime = false;

        chaseAudio = GetComponent<AudioSource>();
        chaseAudio.Stop();
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //2022

        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<AI1Character>();

        agent.updateRotation = false;
        agent.updatePosition = true;

        chase = false;
    }

    void Update()
    {
        distance = Vector3.Distance(target.position, gameObject.transform.position);

        if (distance <= interactionRange)
        {
            exitTimeTrigger = false;
            timeChase = maxTimeChase;
            chase = true;
            
        }
        else exitTimeTrigger = true;

        if (chase)
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            //2022
            isMoving = true;
            AnimPlay();
            //2022              
            // 2020 s
            if (target != null)
                agent.SetDestination(target.position);
            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);
            // 2020 e
        } else
        {
            if (timeChase < maxTimeChase)
            {
                resetTime = true;
            }
        }
        

        if (exitTimeTrigger)
        {
            timeChase -= Time.deltaTime;
            
        }

        if (timeChase <= 0)
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            //2022
            isMoving = false;
            AnimPlay();
            //2022
            resetTime = true;
            chase = false;
        }

        if (resetTime)
        {
            timeChase = maxTimeChase;
        }
        if (timeChase == maxTimeChase)
        {
            resetTime = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTooNear = true;
            isMoving = false;
            AnimPlay();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTooNear = false;
            AnimPlay();
        }
    }

    // 2020 s
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    // 2020 e

    void AnimPlay()
    {
        if (isMoving) animatorAi.SetBool(isMoveName, true);
        if (!isMoving) animatorAi.SetBool(isMoveName, false);

        if (isSneakPeeking) animatorAi.SetBool(isSneakPeekName, true);
        if (!isSneakPeeking) animatorAi.SetBool(isSneakPeekName, false);

        if (isTooNear) animatorAi.SetBool(isTooNearName, true);
        if (!isTooNear) animatorAi.SetBool(isTooNearName, false);
    }

    void AudioPlay()
    {
        if (isMoving) chaseAudio.Play();
        if (!isMoving) chaseAudio.Stop();
    }
}
