using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator anim;



    private void Start()
    {
        anim = GetComponent<Animator>();
    }   

    public void SetAnimatorSpeed(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    public void SetAnimatorJump(bool jump)
    {
        anim.SetBool("Jump", jump);
    }

    public void SetAnimatorLanding()
    {
        anim.SetTrigger("Landing");
    }

    public void SetAnimatorCrouch(bool crouch)
    {
        anim.SetBool("Crouch", crouch);
    }

}
