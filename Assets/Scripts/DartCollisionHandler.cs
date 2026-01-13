using UnityEngine;

public class DartCollisionHandler : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider collider;
    bool isStuck = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<MeshCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isStuck) return;
        if (collision.gameObject.CompareTag("Board"))
        {
            StickToBoard(collision);
        }
    }

    private void StickToBoard(Collision collision)
    {
        isStuck = true;

        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        Transform parentTransform = transform.parent;
        rb.transform.SetParent(parentTransform);

        transform.position += transform.forward * 0.04f;
    }
}
