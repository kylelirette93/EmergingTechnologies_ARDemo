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

        Vector3 targetPosition = new Vector3(arCamera.transform.position.x, transform.position.y, arCamera.transform.position.z);
        transform.LookAt(targetPosition);
    }
}
