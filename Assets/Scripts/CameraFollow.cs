using UnityEngine;

// камера плавно следует за игроком.

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothFactor;

    private Vector3 cameraOffset;
    

    void Start()
    {
        cameraOffset = transform.position - target.position;
    }


    void LateUpdate()
    {
        Vector3 newPosition = target.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
