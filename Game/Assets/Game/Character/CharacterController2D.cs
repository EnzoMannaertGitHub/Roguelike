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

    const float k_GroundedRadius = .2f;             // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;                 // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f;              // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;              // For determining which way the player is currently facing.

    private bool _isJumping = false;
    int _jumpframes = 0;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
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

        if (m_Grounded)
            HandleJump(jump);
    }

    void HandleJump(bool jump)
    {
        // If the player should jump...
        if (jump)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
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