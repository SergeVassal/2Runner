using UnityEngine;

[RequireComponent(typeof(CharacterAnimationController))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{    
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private int maxJumpCount;
    [SerializeField] private float gravityMultiplier;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float ceilingCheckRadius;    

    private CharacterAnimationController animController;
    private MovementStrategyAbstract movementStrategy;    
    


    private void Start()
    {        
        animController = GetComponent<CharacterAnimationController>();
        SetMovementStrategy();
    }

    private void SetMovementStrategy()
    {
        movementStrategy = new MovementDynamicRBodyVelocityChange(this);
    }

    public float GetWalkSpeed()
    {
        return walkSpeed;
    }
    public float GetCrouchSpeed()
    {
        return crouchSpeed;
    }
    public float GetJumpSpeed()
    {
        return jumpSpeed;
    }
    public int GetMaxJumpCount()
    {
        return maxJumpCount;
    }
    public float GetGravityMultiplier()
    {
        return gravityMultiplier;
    }
    public Transform GetGroundCheckTransform()
    {
        return groundCheck;
    }
    public LayerMask GetWhatIsGroundLayerMask()
    {
        return whatIsGround;
    }

    private void Update()
    {
        movementStrategy.GetJumpButtonInput();   
    }        

    private void FixedUpdate()
    {        
        movementStrategy.MoveRigidBody();
        movementStrategy.UpdateAnimationController();
    }
}