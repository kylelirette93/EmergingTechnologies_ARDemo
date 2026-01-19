using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CatMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject interactionParticlesPrefab;
    private AnimationController animationController;
    Vector3 targetPosition;
    private Camera arCamera;
    private AudioSource audioSource;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask catLayer;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement Settings")]
    private float moveSpeed = 1.5f;
    private float rotSpeed = 5f;

    private void Start()
    {
        arCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        targetPosition = transform.position;
        animationController = GetComponent<AnimationController>();
    }

    public void Update()
    {
        HandleTouchInput();
        HandleMovement();
    }

    private void HandleTouchInput()
    {
        if (Touchscreen.current == null || arCamera == null)
            return;

        TouchControl touch = Touchscreen.current.primaryTouch;

        if (!touch.press.wasPressedThisFrame)
            return;

        Vector2 touchPosition = touch.position.ReadValue();

        Ray ray = arCamera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f, catLayer))
        {
            if (hit.transform == transform)
            {
                animationController.PetCat();
                audioSource.Play();
            }
        }

        else if (Physics.Raycast(ray, out RaycastHit groundHit, 50f, groundLayer))
        {
            targetPosition = groundHit.point;
            animationController.SetWalking(true);
            SpawnParticles(groundHit.point);
        }
    }

    private void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            animationController.SetWalking(false);
        }
    }

    private void SpawnParticles(Vector3 hitPoint)
    {
        Instantiate(interactionParticlesPrefab, hitPoint, Quaternion.identity);
    }
}
