using UnityEngine;

public class SmoothToTarget : MonoBehaviour
{
    float movementStep = 0.1f;
    [SerializeField] Vector3 targetPosition;
    public Transform targetTransform;

    void Update()
    {
        targetPosition = targetTransform.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementStep);
    }
}
