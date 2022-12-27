using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    string player = "Player";

    [Header("Check Boxes")]
    [SerializeField] bool isEventsInScene;
    [SerializeField] bool isToF2;
    [SerializeField] bool isF2PhoneRings;
    [SerializeField] bool isFallingAction;
    [SerializeField] bool isGfPhone;

    [Header("Asign")]
    public Events events;
    [Header("Asign Trigger")]
    public GameObject triggerToF2;
    public GameObject triggerF2Phone;
    public GameObject triggerFalling;
    public GameObject triggerGfPhone;

    void Start()
    {
        if (isEventsInScene) events = GameObject.FindGameObjectWithTag("Actions").GetComponent<Events>();
        triggerGfPhone.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player))
        {
            if (isToF2) 
            {
                Debug.Log("to F2");
                events.isToF2Stairway = true;
                triggerToF2.SetActive(false);
            }
            if (isF2PhoneRings)
            {
                Debug.Log("F2 Phone Rings");
                events.isF2PhoneRinging = true;
                triggerF2Phone.SetActive(false);
                triggerGfPhone.SetActive(true);
            }
            if (isGfPhone)
            {
                Debug.Log("Phone Dead, sees Blood, Hears -Cole- Scream");
                events.isToGfPhone = true;
                triggerGfPhone.SetActive(false);
            }


            if (isFallingAction)
            {
                Debug.Log("Falling Scene");
                events.isFalling = true;
                triggerFalling.SetActive(false);
            }
        }
    }
}
