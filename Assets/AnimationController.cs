using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    private Camera arCamera;
    [SerializeField] LayerMask catLayer;
    [SerializeField] LayerMask groundLayer;
    private AudioSource audioSource;
    Vector3 targetPosition;
    private bool isMoving = false;
    private float moveSpeed = 1.5f;
    private float rotSpeed = 5f;
    private FaceCamera faceCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
        arCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        targetPosition = transform.position;
        faceCamera = GetComponent<FaceCamera>();
    }

    public void PetCat()
    {
        animator.SetTrigger("Pet");
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
                animator.SetTrigger("Pet");
                audioSource.Play();
            }
        }

        if (Physics.Raycast(ray, out RaycastHit groundHit, 50f, groundLayer))
        {
            targetPosition = groundHit.point;
            isMoving = true;
            animator.SetBool("isWalking", true);
        }
    }

    private void HandleMovement()
    {
        if (!isMoving)
        {
            faceCamera.enabled = true;
            return;
        }

        faceCamera.enabled = false;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            animator.SetBool("isWalking", false);
        }
    }
}
