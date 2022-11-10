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

    float depth;
    float maxDepth = 100f;

    public Volume volume;

    public void Update()
    {
        rayCast = new Ray(transform.position, transform.forward * maxDepth);

        if (Physics.Raycast(rayCast, out rayHit, maxDepth)) depth = Vector3.Distance(transform.position, rayHit.point);
        else
        {
            if (depth < maxDepth) depth++;
        }
        SetFocus();
    }

    void SetFocus()
    {
        if (volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField))
            depthOfField.focusDistance.Override(depth);
    }
}
