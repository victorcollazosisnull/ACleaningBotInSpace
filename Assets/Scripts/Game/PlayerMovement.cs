using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.IK;
public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    [Header("Movimiento")]
    private Vector2 inputMovement;
    public float speed;
    public float verticalSpeed = 5f;
    private float verticalInput;

    [Header("Teletransporte y Salto")]
    public float distance = 4.29f;
    public Transform playerTransform;
    public float jumpForce = 5f;
    public float horizontalJumpForce = 7f;
    private bool isOnLeftSide = true;
    private float lastVerticalInput;

    [Header("Suelo y Gravedad")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;
    public Transform leftWall;
    public Transform rightWall;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D.gravityScale = 0;
    }
    void Start()
    {
        
    }

    void Update()
    {
        CheckIfGrounded();
    }
    private void CheckIfGrounded()
    {
        float distanceToWall;

        if (isOnLeftSide)
        {
            distanceToWall = Mathf.Abs(transform.position.x - -0.844f);
        }
        else
        {
            distanceToWall = Mathf.Abs(transform.position.x - 0.844f);
        }

        isGrounded = distanceToWall < 0.05f;
    }

    private void Teleport()
    {
        Vector2 targetPosition = new Vector2(-transform.position.x, transform.position.y);
        transform.position = targetPosition;

        isOnLeftSide = !isOnLeftSide;

        if (isOnLeftSide)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = lastVerticalInput < 0;
        }
        else
        {
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = lastVerticalInput < 0;
        }

        AudioManager.Instance.PlayTeleportSound();
    }

    private void Move(float direction)
    {
        if (direction != 0)
        {
            lastVerticalInput = direction; 

            Vector3 moveDirection;

            if (direction > 0)
            {
                moveDirection = Vector3.up;

                if (isOnLeftSide)
                {
                    spriteRenderer.flipX = false;
                    spriteRenderer.flipY = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                    spriteRenderer.flipY = false;
                }
            }
            else
            {
                moveDirection = Vector3.down;

                if (isOnLeftSide)
                {
                    spriteRenderer.flipX = false;
                    spriteRenderer.flipY = true;
                }
                else
                {
                    spriteRenderer.flipX = true;
                    spriteRenderer.flipY = true;
                }
            }

            playerTransform.position += moveDirection * verticalSpeed * Time.fixedDeltaTime;
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = Vector2.zero;

        if (Mathf.Abs(inputMovement.x) > 0.01f)
        {
            if (isOnLeftSide)
            {
                _rigidbody2D.AddForce(new Vector2(-horizontalJumpForce, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody2D.AddForce(new Vector2(horizontalJumpForce, jumpForce), ForceMode2D.Impulse);
            }
        }
        else
        {
            if (isOnLeftSide)
            {
                _rigidbody2D.AddForce(new Vector2(-horizontalJumpForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody2D.AddForce(new Vector2(horizontalJumpForce, 0), ForceMode2D.Impulse);
            }
        }

        AudioManager.Instance.PlayJumpSound();
    }

    private void ApplyWallAttraction()
    {
        float targetXPosition = isOnLeftSide ? leftWall.position.x : rightWall.position.x;
        Vector2 attractionForce = new Vector2(targetXPosition - transform.position.x, 0);
        _rigidbody2D.AddForce(attractionForce * 10f);
    }

    public void ReadDirection(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f) return;

        verticalInput = context.ReadValue<float>();
    }

    public void ReadTeleport(InputAction.CallbackContext context)
    {
        if (PauseMenu.GamePaused)
            return;

        if (Time.timeScale == 0f) return;
        if (context.performed)
        {
            Teleport();
        }
    }

    public void ReadJump(InputAction.CallbackContext context)
    {
        if (PauseMenu.GamePaused)
            return;

        if (Time.timeScale == 0f) return;
        if (context.performed && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (PauseMenu.GamePaused)
            return;

        Move(verticalInput);
        if (!isGrounded)
        {
            ApplyWallAttraction();
        }
    }
}
