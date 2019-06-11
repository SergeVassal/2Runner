﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDynamicRBodyVelocityChange : MovementStrategyAbstract
{
    private Vector2 targetRBodyVelocity=Vector2.zero;
    private float previousRBodyVelocityY;  
    


    public MovementDynamicRBodyVelocityChange(CharacterController charController) : base(charController)
    {
    }  

    protected override void AddHorizontalSpeedToRigidBody()
    {        
        float speed = GetMovementSpeed();
        targetRBodyVelocity = new Vector2(horizontalInputRaw * speed, rBody.velocity.y);        
    }
    private float GetMovementSpeed()
    {
        float speed = isCrouching ? crouchSpeed : walkSpeed;
        return speed;
    }  

    protected override void AddJumpForceIfNeeded()
    {
        if (isJumpPressed && !hasJumpedDuringThisUpdate)
        {
            if (currJumpCount < maxJumpCount)
            {
                targetRBodyVelocity.y = jumpSpeed;
                hasJumpedDuringThisUpdate = true;
                currJumpCount += 1;
            }
            
        }       
    }

    protected override void StopRBodyIfGrounded()
    {
        if (isGrounded)
        {
            targetRBodyVelocity.y = 0f;
        }
        previousRBodyVelocityY = targetRBodyVelocity.y;
    }

    protected override void AddGravityIfNeeded()
    {      
        if (!isJumpPressed)
        {
            if (!isGrounded)
            {
                targetRBodyVelocity.y = previousRBodyVelocityY + Physics.gravity.y * gravityMultiplier;                
            }                       
        }
        previousRBodyVelocityY = targetRBodyVelocity.y;
    }

    protected override void ApplyRbodyMovement()
    {        
        rBody.velocity = targetRBodyVelocity;        
    }

    protected override void FlipCharacterIfNeeded()
    {
        if (rBody.velocity.x > 0 && !facingRight)
        {
            DoTransformXFlip();
        }
        else if (rBody.velocity.x < 0 && facingRight)
        {
            DoTransformXFlip();
        }
    }    
    
}
