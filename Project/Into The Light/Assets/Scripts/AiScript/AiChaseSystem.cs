using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AI1Character))]

public class AiChaseSystem : MonoBehaviour
{
    [Header("AI Anim Controller")]
    static Animator animatorAi;

    [SerializeField] bool isMoving = false;
    [SerializeField] bool isSneakPeeking = false;
    [SerializeField] bool isStare = false;
    [SerializeField] bool isKill = false;

    string isMoveName = "isWalk";
    string isSneakPeekName = "isSneakPeek";
    string isTooNearName = "isToNear";
    string isKillName = "isKill";

    float distance;
    [SerializeField] float interactionRange = 3f;
    [Header("Chase Value")]
    public float timeChase = 30.0f;
    public float maxTimeChase = 30.0f;

    public bool chase = false;
    public bool exitTimeTrigger = false;
    public bool resetTime = false;

    [Header("Stare To Kill Value")]
    public float timeKill = 15.0f;
    public float maxTimeKill = 15.0f;

    public bool stare = false;
    public bool kill = false;
    public bool exitKillTimeTrigger = false;
    public bool resetKillTime = false;

    AudioSource chaseAudio;

    // 2020 test start
    public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
    public AI1Character character { get; private set; } // the character we are controlling
    public Transform target; // target to aim for
    // 2020 test end

    void Start()
    {
        animatorAi = GetComponent<Animator>();
        chaseAudio = GetComponent<AudioSource>();
        character = GetComponent<AI1Character>();
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        agent = GetComponentInChildren<NavMeshAgent>();
        chaseAudio.Stop();
        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    void Update()
    {
        distance = Vector3.Distance(target.position, gameObject.transform.position);

        //Chase
        if (distance <= interactionRange)
        {
            exitTimeTrigger = false;
            timeChase = maxTimeChase;
            chase = true;
        }
        else exitTimeTrigger = true;

        //Timer
        if (chase) Chase();
        else if (timeChase < maxTimeChase) resetTime = true;

        if (exitTimeTrigger) timeChase -= Time.deltaTime;

        if (timeChase <= 0)
        {
            IdleState();
            AnimPlay();
        }

        if (resetTime) timeChase = maxTimeChase;
        if (timeChase == maxTimeChase) resetTime = false;

        //Stare
        if (stare) StareState();
        else if (timeKill < maxTimeKill) resetKillTime = true;

        if (exitKillTimeTrigger) timeKill -= Time.deltaTime;
        if (!exitKillTimeTrigger) isStare = false;

        //Kill
        if(timeKill < 0)
        {
            KillState();
            AnimPlay();
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (resetKillTime) timeKill = maxTimeKill;
        if (timeKill == maxTimeKill) //////////////////////////////+ // 15 walk again after kill
        {
            resetKillTime = false;
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stare = true;
            exitKillTimeTrigger = true;
            isMoving = false; //
            AnimPlay();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stare = false;
            exitKillTimeTrigger = false;
            isMoving = true;
            Chase();
            AnimPlay();           
        }
    }

    // 2020 s ???????????????
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void StareState()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        isStare = true;
        resetTime = true;
        chase = false;
    }
    void KillState()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        isKill = true;
        resetTime = true;
        chase = false;
    }
    void IdleState()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        isMoving = false;
        resetTime = true;
        chase = false;
    }

    void Chase()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        isMoving = true;
        AnimPlay();
        if (target != null) agent.SetDestination(target.position);
        if (agent.remainingDistance > agent.stoppingDistance) character.Move(agent.desiredVelocity, false, false);
        else character.Move(Vector3.zero, false, false);
    }

    void AnimPlay()
    {
        if (isMoving) animatorAi.SetBool(isMoveName, true);
        if (!isMoving) animatorAi.SetBool(isMoveName, false);

        if (isSneakPeeking) animatorAi.SetBool(isSneakPeekName, true);
        if (!isSneakPeeking) animatorAi.SetBool(isSneakPeekName, false);

        if (isStare) animatorAi.SetBool(isTooNearName, true);
        if (!isStare) animatorAi.SetBool(isTooNearName, false);

        if (isKill) animatorAi.SetBool(isKillName, true);
        if (!isKill) animatorAi.SetBool(isKillName, false);
    }

    void AudioPlay()
    {
        if (isMoving) chaseAudio.Play();
        if (!isMoving) chaseAudio.Stop();
    }
}
