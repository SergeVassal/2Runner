using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimationController))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;

    [SerializeField] private float groundRadius;
    [SerializeField] private float ceilingRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private int maxJumpCount;
    [SerializeField] private float jumpPower;

    private Rigidbody2D rBody;
    private bool facingRight = true;
    private bool grounded;
    private bool jumping=false;
    private bool crouch;
    private bool previouslyGrounded;
    private int jumpCount = 0;
    private CharacterAnimationController animController;
    


    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animController = GetComponent<CharacterAnimationController>();
    }


    private void Update()
    {
        grounded= Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);        

        crouch = CrossPlatformInputManager.GetButton("Crouch");
        animController.SetAnimatorCrouch(crouch);

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {            
            JumpFunc();
        }  

        if (!previouslyGrounded && grounded)
        {
            jumpCount = 0;
            jumping = false;
            animController.SetAnimatorJump(jumping);
            animController.SetAnimatorLanding();
            rBody.velocity = new Vector2(rBody.velocity.x, 0f);
        }  

        previouslyGrounded = grounded;
    }


    private void FixedUpdate()
    {    
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;                
            }
        }

        MoveRigidbody();
    }


    private void MoveRigidbody()
    {
        float horInputValue = CrossPlatformInputManager.GetAxis("Horizontal");

        //float moveVelocity = crouch?(horInputValue * crouchSpeed) :(horInputValue * walkSpeed);

        float moveVelocity = crouch ? (crouchSpeed) : (walkSpeed);

        animController.SetAnimatorSpeed(Mathf.Abs(moveVelocity));        

        if (moveVelocity > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveVelocity < 0 && facingRight)
        {
            Flip();
        }

        rBody.velocity = new Vector2(moveVelocity, rBody.velocity.y);
    }


    private void JumpFunc()
    {        
        if (jumpCount < maxJumpCount)
        {
            rBody.AddForce(new Vector2(rBody.velocity.x, jumpPower));
            rBody.velocity = new Vector2(rBody.velocity.x, 0f);
            jumpCount += 1;
            jumping = true;
            animController.SetAnimatorJump(jumping);
        }
        else
        {
            return;
        }
    }


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

}
