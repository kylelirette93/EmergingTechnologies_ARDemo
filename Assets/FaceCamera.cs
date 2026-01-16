using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera arCamera;

    private void Awake()
    {
        arCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (!arCamera) return;

        transform.LookAt(transform.position + arCamera.transform.rotation * Vector3.forward,
            arCamera.transform.rotation * Vector3.up
            );

        transform.Rotate(0, 180f, 0, Space.Self);
    }
}
