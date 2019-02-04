using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;

    private Rigidbody2D rBody;
    private bool facingRight=true;
    



    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        /*
         * If 
         * 
         */ 
    }
    

    private void FixedUpdate()
    {
        MoveRigidbody();   
    }


    private void MoveRigidbody()
    {
        float horInputValue = ProtoCrossPlatformInputManager.GetAxis("Horizontal");
        float moveVelocity = horInputValue*walkSpeed;

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


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

}
