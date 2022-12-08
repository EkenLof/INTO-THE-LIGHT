using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerOnOff : MonoBehaviour
{
    [SerializeField] float timerSceneFade = 8f;


    void Update()
    {
        timerSceneFade -= Time.deltaTime;

        if (timerSceneFade <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
