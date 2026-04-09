using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private float jumpCooldown = 1.0f;
    [SerializeField] private GameObject opponentPlayer;

    private bool isMoving;
    private Vector2 moveDirection;
    private bool isJumping = false;
    private bool facingRight = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    private Vector3 playerScreenPosition;

    private Animator animator;
    private Rigidbody rb;
    private AnimatorStateInfo currentAnimationInfo;

    private const int FlipLayerIndex = 1;

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
            isMoving = true;
        }
        else if(context.canceled)
        {
            isMoving = false;
        }
    }

    private void Awake() {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentAnimationInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(Camera.main != null)
        {
            playerScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        }

        if(playerScreenPosition.x < 0)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }

        if(playerScreenPosition.x > Screen.width)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }

        if(opponentPlayer != null)
        {
            Vector3 opponentPosition = opponentPlayer.transform.position;
            if(this.transform.position.x < opponentPosition.x)
            {
                FaceRight();
            }
            else
            {
                FaceLeft();
            }
        }

        if (isMoving)
        {
            if (currentAnimationInfo.IsTag("Moving"))
            {
                if (moveDirection.x > 0 && canMoveRight)
                {
                    animator.SetBool("Forward", true);
                    this.transform.Translate(Vector3.right * moveSpeed);
                }
                else if (moveDirection.x < 0 && canMoveLeft)
                {
                    animator.SetBool("Backward", true);
                    this.transform.Translate(Vector3.left * moveSpeed);
                }
            }
            else if (moveDirection.x == 0)
            {
                animator.SetBool("Forward", false);
                animator.SetBool("Backward", false);
            }

            if (moveDirection.y > 0 && !isJumping)
            {
                isJumping = true;
                animator.SetTrigger("Jump");
                StartCoroutine(JumpCooldown());
            }
            else if (moveDirection.y < 0)
            {
                animator.SetBool("Crouch", true);
            }
            else
            {
                animator.SetBool("Crouch", false);
            }
        }
        else
        {
            animator.SetBool("Forward", false);
            animator.SetBool("Backward", false);
            animator.SetBool("Crouch", false);
        }

    }

    private void FaceLeft()
    {
        if (facingRight)
        {
            facingRight = false;
            transform.GetChild(0).Rotate(0, 180, 0);
            animator.SetLayerWeight(FlipLayerIndex, 1);
        }
    }

    private void FaceRight()
    {
        if (!facingRight)
        {
            facingRight = true;
            transform.GetChild(0).Rotate(0, 180, 0);
            animator.SetLayerWeight(FlipLayerIndex, 0);
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        isJumping = false;
    }
}
