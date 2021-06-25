using UnityEngine;
using UnityEngine.Events;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = 10f;                       // Speed at which the player moves horizontally
    [SerializeField] private float m_Jumpacceleration = 50f;                    // Amount of force added when the player jumps.
    [SerializeField] private float m_JumpForce = 0f;                            // Amount of force added when the player jumps.
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f;             // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;                 // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f;              // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;              // For determining which way the player is currently facing.

    private float m_JumpMeterMax = 2.5f;            //How long the player can jump
    private float m_CurrentJumpMeter = 2.5f;        //Current jump energy

    bool _StartCooldown = false;
    float _StartCooldownSec = 1.5f;
    float _ElapsedStartCooldownSec = 0f;
    bool _isJumping = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        if (!_isJumping)
        {
            _ElapsedStartCooldownSec += Time.deltaTime;
            if (_ElapsedStartCooldownSec > _StartCooldownSec)
                _StartCooldown = true;
        }
        else
        {
            _ElapsedStartCooldownSec = 0;
            _StartCooldown = false;
        }

        if (_StartCooldown && m_CurrentJumpMeter < m_JumpMeterMax)
        {
            m_CurrentJumpMeter += Time.deltaTime / 2f;
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
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

    public void Move(float move, bool crouch, bool jump)
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

        HandleJump(jump);
    }

    void HandleJump(bool jump)
    {
        // If the player should jump...
        if (jump)
        {
            if (m_CurrentJumpMeter > 0)
            {
                _isJumping = true;
                _StartCooldown = false;

                m_CurrentJumpMeter -= Time.fixedDeltaTime;

                m_Grounded = false;
                // Add a vertical force to the player.
                float jumpstrength = 7f;
                m_JumpForce = jumpstrength + m_Jumpacceleration * Time.fixedDeltaTime;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
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
        m_Rigidbody2D.AddForce(direction * 40);
    }
}