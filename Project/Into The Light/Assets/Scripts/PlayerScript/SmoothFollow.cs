using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform targetFollow;
    [SerializeField] private float smoothTime = .125f;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - targetFollow.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = targetFollow.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
    }
}