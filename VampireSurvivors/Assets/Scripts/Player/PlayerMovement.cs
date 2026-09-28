using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [Header("Required Components")]
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 lastMoveDirection;

    private PlayerHealth playerHealthScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerHealthScript = GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateAnimation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        float horizontal = moveInput.x;
        float vertical = moveInput.y;

        if ((horizontal == 0 && vertical == 0) && (moveInput.x != 0 || moveInput.y != 0))
        {
            lastMoveDirection = moveInput;
        }

        //Make sure diagonal movement is not faster
        moveInput.Normalize();
    }

    private void MovePlayer()
    {
        //Check if dead, yes then return
        if (playerHealthScript.CheckIfDead() == true)
        {
            //Stop player from moving completely
            rb.linearVelocity = Vector2.zero;

            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void UpdateAnimation()
    {
        //Walking animation
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);

        //Trigger idle/walk
        animator.SetFloat("MoveMagnitude", moveInput.magnitude);

        //Idle animation
        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }
}