using UnityEngine;
using UnityEngine.InputSystem;

public class Dart : MonoBehaviour
{
    Rigidbody rb;
    float throwForce = 13f;
    float upwardArch = 0.15f;
    bool isThrown = false;
    Camera cam;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.isKinematic = true;

        if (cam == null) cam = Camera.main;

    }
    private void Update()
    {
        if (!isThrown)
        {
            FollowMouse();
            if (Input.GetMouseButtonDown(0))
            {
                ThrowDart();
            }
        }
    }

    private void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0.3f;

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePosition);
        transform.position = worldPos;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Quaternion lookRotation = Quaternion.LookRotation(ray.direction);
        transform.rotation = lookRotation * Quaternion.Euler(0f, 180f, 0f);
    }

    private void ThrowDart()
    {
        isThrown = true;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Vector3 throwDirection = ray.direction;
        throwDirection.y += upwardArch;

        rb.isKinematic = false;
        Quaternion throwRotation = Quaternion.LookRotation(throwDirection);
        rb.transform.rotation = throwRotation * Quaternion.Euler(0f, 180f, 0f);
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
