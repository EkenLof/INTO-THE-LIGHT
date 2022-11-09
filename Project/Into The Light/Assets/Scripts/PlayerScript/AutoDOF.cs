using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class AutoDOF : MonoBehaviour
{
    Ray rayCast;
    RaycastHit rayHit;

    bool isHit;
    float depth;

    public Volume volume;
    DepthOfField depthOfField;

    private void Start()
    {
        volume.profile.TryGet(out depthOfField);
    }

    public void Update()
    {
        rayCast = new Ray(transform.position, transform.forward * 100);
        isHit = false;

        if (Physics.Raycast(rayCast, out rayHit, 100f))
        {
            isHit = true;
            depth = Vector3.Distance(transform.position, rayHit.point);
            Debug.Log("Hitting");
        }
        else
        {
            if (depth < 100f) depth++;
        }

        SetFocus();
    }

    void SetFocus()
    {
        depthOfField.focusDistance.value = depth;
    }

    private void OnDrawGizmos()
    {
        if (isHit)
        {
            Gizmos.DrawSphere(rayHit.point, 0.1f);
            Debug.DrawRay(transform.position, transform.forward * Vector3.Distance(transform.position, rayHit.point));
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 100f);
        }
    }
}
