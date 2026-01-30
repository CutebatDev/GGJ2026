using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;

public class MovementController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float gravity = 20f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float slopeAngleLimit = 45f;
    [SerializeField] private float groundCheckDistance = 0.1f;

    [Header ("References")]
    [SerializeField] private Rigidbody2D rb;

    private float horizontal;
    private Vector2 velocity;

    void Awake()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        HandleInput();
        HandleHorizontal();
        HandleJump();
        ApplyGravity();
        MovePlayer();
    }


    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);
    }

    private void HandleHorizontal()
    {
        float translation = horizontal * moveSpeed * Time.deltaTime;
        //float newX = translation + transform.position.x;
        //transform.position = new Vector2(newX, transform.position.y);

        Vector2 move = new Vector2(translation, 0f);

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance + 0.1f, groundLayer);
        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeAngle <= slopeAngleLimit && horizontal != 0)
            {
                Vector2 slopeDir = new Vector2(hit.normal.y, - hit.normal.x);
                move = slopeDir * translation;
            }
        }

        velocity.x = move.x;
    }

    private void MovePlayer()
    {
        if (!IsGrounded())
            return;

        if (velocity.y < 0)
        {
            velocity.y = 0f;
        }

        rb.MovePosition(rb.position + velocity * Time.deltaTime);
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

    private void ApplyGravity()
    {
        if (IsGrounded())
            return;

        velocity.y -= gravity * Time.deltaTime;
    }
}
