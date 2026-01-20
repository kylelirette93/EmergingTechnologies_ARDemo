using UnityEngine;
using UnityEngine.EventSystems;

public class BladeController : MonoBehaviour
{
    Transform cat;
    Vector3 targetPosition;
    Vector3 moveDirection;
    float moveSpeed = 0.7f;
    float launchForce = 3f;

    private void OnEnable()
    { 
        cat = GameObject.FindWithTag("Cat").transform;
        targetPosition = cat.position;
        moveDirection = (targetPosition - transform.position).normalized;
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            GameManager.Instance.HandleStateChange(GameState.Gameover);
            Destroy(gameObject);
        }
    }
}
