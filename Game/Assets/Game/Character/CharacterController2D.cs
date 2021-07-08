using UnityEngine;
using UnityEngine.Events;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = 10f;                       // Speed at which the player moves horizontally
    [SerializeField] private float m_Jumpacceleration = 50f;                    // Amount of force added when the player jumps.
    [SerializeField] private float m_JumpForce = 10f;                            // Amount of force added when the player jumps.
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] private Animator m_Animator;

    const float k_GroundedRadius = .07f;             // Radius of the overlap circle to determine if grounded
    public bool m_Grounded = true;                 // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f;              // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;              // For determining which way the player is currently facing.

    private int _jumpCount = 0;

    private bool _isJumping = false;
    private bool _isDoubleJumping = false;
    int _jumpframes = 0;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Animator.SetBool("Grounded", wasGrounded);
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool crouch, bool jump, bool doubleJump)
    {
        _isJumping = jump;
        // Only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Enable the collider when not crouching
            if (m_CrouchDisableCollider != null)
                m_CrouchDisableCollider.enabled = true;

            // Update rigidbody velocity
            m_Rigidbody2D.velocity = new Vector2(move * m_MovementSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        if (m_Grounded)
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
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_Animator.SetTrigger("Jump");
        }
    }
    void HandleDoubleJump(bool jump)
    {
        // If the player should jump...
        if (jump)
        {
            if (_isDoubleJumping)
                return;

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            _isDoubleJumping = true;
            m_Animator.SetTrigger("DoubleJump");
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Knockback(Vector2 direction)
    {
        m_Rigidbody2D.AddForce(direction * 200);
    }

    public void Roll(bool isleft)
    {
        if (isleft)
        {
            m_Rigidbody2D.AddForce(new Vector2(-75, 0));
        }
        else
        {
            m_Rigidbody2D.AddForce(new Vector2(75, 0));
        }
    }
}