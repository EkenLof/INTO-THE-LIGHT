using System;
using UnityEngine;

//[RequireComponent(typeof (NavMeshAgent))]

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(AI1Character))]

public class AiChaseSystem : MonoBehaviour
{

    public float timeChase = 15.0f;
    public float maxTimeChase = 15.0f;
    public bool exitTimeTrigger;
    public bool resetTime;
    private string animFadeSmoth = "Armature|Idle";

    AudioSource idleAudio;

    // 2020 test start
    public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
    public AI1Character character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for

    public bool chase;
    public bool chaseTriggerEvent;
    // 2020 test end


    void Start()
    {
        // 2020 s
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<AI1Character>();

        agent.updateRotation = false;
        agent.updatePosition = true;

        chase = false;
        chaseTriggerEvent = false;
        // 2020 e

        idleAudio = GetComponent<AudioSource>();
        idleAudio.Stop();
        GetComponent<Animation>()[animFadeSmoth].speed = 1;
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Animation>().Play("Armature|Idle");
        GetComponent<Animation>()["Armature|Idle"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>()["Armature|Run"].wrapMode = WrapMode.Loop;
        exitTimeTrigger = false;
        resetTime = false;
        GetComponent<Animation>().CrossFade(animFadeSmoth);
    }

    void Update()
    {
        if (chaseTriggerEvent)
        {
            chase = true;
            idleAudio.Play();
        }

        if (chase)
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

            GetComponent<Animation>().Play("Armature|Run");
            
            
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
            GetComponent<Animation>().Play("Armature|Idle");
            resetTime = true;
            idleAudio.Stop();

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
            exitTimeTrigger = false;
            timeChase = maxTimeChase;
            chase = true;
            idleAudio.Play();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            exitTimeTrigger = true;
        }
    }

    // 2020 s
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    // 2020 e
}
