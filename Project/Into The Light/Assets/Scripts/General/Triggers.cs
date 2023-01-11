using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    string player = "Player";

    [Header("Check Boxes")]
    [SerializeField] bool isEventsInScene;
    [SerializeField] bool isToF2;
    [SerializeField] bool isF2PhoneRings;
    [SerializeField] bool isGfPhone;
    [SerializeField] bool isFallingAction;

    [Header("Asign")]
    [SerializeField] Events events;
    [Header("Asign Trigger")]
    public GameObject triggerToF2;
    public GameObject triggerF2Phone;
    public GameObject triggerGfPhone;
    public GameObject triggerFalling;
    

    void Start()
    {
        if (isEventsInScene) events = GameObject.FindWithTag("Actions").GetComponent<Events>();
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

            }
            if (isGfPhone && events.steps == 7)
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
