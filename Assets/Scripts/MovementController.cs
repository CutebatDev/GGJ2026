using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;

public class MovementController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;

    [Header ("References")]
    [SerializeField] private Rigidbody2D rb;
    private float horizontal;

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleJump();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);
    }

    private void HandleMovement()
    {
        float translation = horizontal * moveSpeed * Time.deltaTime;
        float newX = translation + transform.position.x;
        transform.position = new Vector2(newX, transform.position.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0f,
            groundLayer
        );
    }

    private void HandleJump()
    {
        if (!IsGrounded())
            return;

        if (Input.GetButtonDown("Jump"))
        {
            rb.linearVelocityY += jumpForce;
        }
    }
}
