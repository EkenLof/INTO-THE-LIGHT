using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardReaders : MonoBehaviour
{
    string player = "Player";

    [Header("Check Boxes")]
    [SerializeField] bool isCardReaderMensLocker;

    [Header("Asign")]
    public Events events;


    void Start()
    {
        events = GameObject.FindGameObjectWithTag("Actions").GetComponent<Events>();
    }


    void Update()
    {
        bool leftClickDown = Input.GetMouseButtonDown(0);

    }
}
