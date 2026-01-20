using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

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
    bool isMoving = false;

    [Header("Jump Settings")]
    private bool isGrounded = false;
    private bool isJumping = false;
    float jumpForce = 7f;
    Rigidbody rb;
    Quaternion originalRotation;

    private void Start()
    {
        arCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        targetPosition = transform.position;
        animationController = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody>();
        originalRotation = transform.rotation;
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
                if (isGrounded)
                {
                    Jump();
                }
                // Gonna play a jump anim here.
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

        RotateInMoveDirection();

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            animationController.SetWalking(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isJumping = false;
            isGrounded = true;
            transform.rotation = originalRotation;
        }
    }

    private void RotateInMoveDirection()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero && !isJumping)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        }
    }

    private void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        transform.rotation = originalRotation;
        isGrounded = false;
    }

    private void SpawnParticles(Vector3 hitPoint)
    {
        Instantiate(interactionParticlesPrefab, hitPoint, Quaternion.identity);
    }
}
