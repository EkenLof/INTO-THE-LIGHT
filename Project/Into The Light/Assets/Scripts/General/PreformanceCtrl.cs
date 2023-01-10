using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreformanceCtrl : MonoBehaviour
{
    string player = "Player";

    [Header("Check Boxes")]
    [SerializeField] bool isGf;
    [SerializeField] bool isF1;
    [SerializeField] bool isF2;
    [SerializeField] bool isFromF2;
    [SerializeField] bool isLRoomHall;
    [SerializeField] bool isToLRoom;
    [SerializeField] bool isWRoomStair;
    [SerializeField] bool isVentilation;
    [SerializeField] bool isFromVentilation;
    [SerializeField] bool isLab;
    [SerializeField] bool isFromLab;

    [Header("Asign Trigger")]
    public GameObject gf;
    public GameObject f1Halls;
    public GameObject f2;
    public GameObject o1Room;
    public GameObject o2Room;
    public GameObject lRoom;
    public GameObject wRoom;
    public GameObject nRoom;
    public GameObject lab;
    public GameObject vent;

    void Start()
    {
        f1Halls.SetActive(false);
        f2.SetActive(false);
        o1Room.SetActive(false);
        o2Room.SetActive(false);
        lRoom.SetActive(false);
        wRoom.SetActive(false);
        nRoom.SetActive(false);
        lab.SetActive(false);
        vent.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player))
        {
            if (isGf)
            {
                gf.SetActive(true);
                f1Halls.SetActive(false);
                f2.SetActive(false);
                o1Room.SetActive(false);
                o2Room.SetActive(false);
                lRoom.SetActive(false);
                wRoom.SetActive(false);
                nRoom.SetActive(false);
            }
            if (isF1)
            {
                f1Halls.SetActive(true);
                o1Room.SetActive(true);
                o2Room.SetActive(true);
                f2.SetActive(true);
            }
            if (isF2)
            {
                gf.SetActive(false);
                o2Room.SetActive(false);
            }
            if (isFromF2)
            {
                gf.SetActive(true);
                o2Room.SetActive(true);
            }
            if (isLRoomHall)
            {
                lRoom.SetActive(true);
                gf.SetActive(false);
                f1Halls.SetActive(true);
                f2.SetActive(false);
                o1Room.SetActive(false);
                o2Room.SetActive(false);
            }
            if (isToLRoom)
            {
                lRoom.SetActive(true);
                gf.SetActive(false);
                f1Halls.SetActive(true);
                wRoom.SetActive(false);
                nRoom.SetActive(false);
                vent.SetActive(false);
            }
            if (isWRoomStair)
            {
                lRoom.SetActive(false);
                f1Halls.SetActive(false);
                wRoom.SetActive(true);
                nRoom.SetActive(true);
                vent.SetActive(true);
            }
            if (isVentilation)
            {
                gf.SetActive(true);
                wRoom.SetActive(false);
                nRoom.SetActive(false);
            }
            if (isFromVentilation)
            {
                gf.SetActive(false);
                wRoom.SetActive(true);
                nRoom.SetActive(true);
            }
            if (isLab)
            {
                lab.SetActive(true);
            }
            if (isFromLab)
            {
                lab.SetActive(false);
            }
        }
    }
}
