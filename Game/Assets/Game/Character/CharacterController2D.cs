using UnityEngine;
using UnityEngine.Events;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private bool _airControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask _whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform _groundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform _ceilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D _crouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] private Animator _animator;

    private float _movementSpeed = 10f;                                        // Speed at which the player moves horizontally
    private float _jumpForce = 10f;                                            // Amount of force added when the player jumps.

    const float _rollForceMultiplier = 4f;
    const float _groundedRadius = .07f;                                        // Radius of the overlap circle to determine if grounded
    public bool _grounded = true;                                              // Whether or not the player is grounded.
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;                                          // For determining which way the player is currently facing.

    private bool _isDoubleJumping = false;
    private bool _isJumping;
    private bool _movementEnabled = true;
    public bool MovementEnabled { get { return _movementEnabled; } set { _movementEnabled = value; } }

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        _movementSpeed = movementSpeed;
    }

    public void SetJumpSpeed(float jumpSpeed)
    {
        _jumpForce = jumpSpeed;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _grounded;
        _animator.SetBool("Grounded", wasGrounded);
        _grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool crouch, bool jump, bool doubleJump)
    {
        if (!_movementEnabled)
            return;

        _isJumping = jump;
        // Only control the player if grounded or airControl is turned on
        if (_grounded || _airControl)
        {
            // Enable the collider when not crouching
            if (_crouchDisableCollider != null)
                _crouchDisableCollider.enabled = true;

            // Update rigidbody velocity
            _rigidbody2D.velocity = new Vector2(move * _movementSpeed, _rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !_facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && _facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        if (_grounded)
        {
            _isDoubleJumping = false;
            HandleJump(jump);
        }
        else
        {
            HandleDoubleJump(doubleJump);
        }
    }

    void HandleJump(bool jump)
    {
        // If the player should jump...
        if (jump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _grounded = false;
            _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
            _animator.SetTrigger("Jump");
        }
    }
    void HandleDoubleJump(bool jump)
    {
        // If the player should jump...
        if (jump)
        {
            if (_isDoubleJumping)
                return;

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _grounded = false;
            _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
            _isDoubleJumping = true;
            _animator.SetTrigger("DoubleJump");
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Knockback(Vector2 direction)
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
        _rigidbody2D.AddForce(new Vector2(direction.x * 10, -direction.y * 50));
    }

    public void Roll(bool isleft)
    {
        if (isleft)
        {
            _rigidbody2D.AddForce(new Vector2(-_movementSpeed * _rollForceMultiplier, 0));
        }
        else
        {
            _rigidbody2D.AddForce(new Vector2(_movementSpeed * _rollForceMultiplier, 0));
        }
    }
}