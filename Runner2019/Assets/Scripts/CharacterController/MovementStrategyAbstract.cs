using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStrategyAbstract
{
    protected CharacterController characterController;
    protected Rigidbody2D rBody;
    protected CharacterAnimationController animController;

    protected float walkSpeed;
    protected float crouchSpeed;
    protected float jumpSpeed;

    protected int maxJumpCount;
    protected float gravityMultiplier;

    protected bool isCrouching;
    protected bool isGrounded;
    protected bool hasJustLanded;    
    protected bool facingRight = true;

    protected float horizontalInputRaw;

    protected bool isJumpPressed;
    protected bool isJumpPressedDuringFixedUpdate;
    protected bool hasJumpedDuringThisUpdate;
    protected bool previouslyGrounded;
    protected int currJumpCount;

    private Transform characterTransform;  
    private Transform groundCheck;
    private LayerMask whatIsGround;
       

    
    public MovementStrategyAbstract(CharacterController characterControllerRef)
    {
        characterController = characterControllerRef;
        rBody = characterControllerRef.GetComponent<Rigidbody2D>();
        animController = characterControllerRef.GetComponent<CharacterAnimationController>();        
        
        walkSpeed = characterControllerRef.GetWalkSpeed();
        crouchSpeed = characterControllerRef.GetCrouchSpeed();
        jumpSpeed = characterControllerRef.GetJumpSpeed();

        maxJumpCount = characterControllerRef.GetMaxJumpCount();
        gravityMultiplier = characterControllerRef.GetGravityMultiplier();

        characterTransform = characterControllerRef.GetComponent<Transform>();               

        groundCheck = characterControllerRef.GetGroundCheckTransform();
        whatIsGround = characterControllerRef.GetWhatIsGroundLayerMask();
    }

    public void MoveRigidBody()
    {
        CheckIfRigidBodyIsGrounded();

        UpdateJustLandedState();

        previouslyGrounded = isGrounded;

        GetCrossPlatformHorizontalInput();

        GetCrouchInput();          

        AddHorizontalSpeedToRigidBody();

        if (!isJumpPressedDuringFixedUpdate)
        {
            StopRBodyIfGrounded();
            AddGravityIfNeeded();
        }

        AddJumpForceIfNeeded();                

        ApplyRbodyMovement();
        FlipCharacterIfNeeded();        
    }

    private void CheckIfRigidBodyIsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);
    }

    private void UpdateJustLandedState()
    {
        if (isGrounded && !previouslyGrounded)
        {
            hasJustLanded = true;
            currJumpCount = 0;
        }
        else if (isGrounded && previouslyGrounded && hasJustLanded)
        {
            hasJustLanded = false;
        }
    }

    private void GetCrossPlatformHorizontalInput()
    {
        horizontalInputRaw = CrossPlatformInputManager.GetAxis("Horizontal");
        //horizontalInputRaw = 0.5f;
    }

    private void GetCrouchInput()
    {
        isCrouching = CrossPlatformInputManager.GetButton("Crouch");        
    }    

    protected abstract void AddHorizontalSpeedToRigidBody();

    protected abstract void StopRBodyIfGrounded();

    protected abstract void AddGravityIfNeeded();

    protected abstract void AddJumpForceIfNeeded();    

    protected abstract void ApplyRbodyMovement();

    public void UpdateAnimationController()
    {
        animController.SetAnimatorSpeed(Mathf.Abs(rBody.velocity.x));

        if (isJumpPressed)
        {
            animController.SetAnimatorJump(true);
        }        

        if (hasJustLanded)
        {
            animController.SetAnimatorJump(false);
            animController.SetAnimatorLanding();
        }
    }    

    protected abstract void FlipCharacterIfNeeded();

    protected void DoTransformXFlip()
    {
        facingRight = !facingRight;
        Vector3 scale = characterTransform.localScale;
        scale.x *= -1f;
        characterTransform.localScale = scale;
    }

    public void GetJumpButtonInput()
    {
        isJumpPressed = CrossPlatformInputManager.GetButtonDown("Jump");
        if (isJumpPressed)
        {
            isJumpPressedDuringFixedUpdate = true;
        }
        if (isJumpPressed)
        {
            hasJumpedDuringThisUpdate = false;
        }
    }
}
