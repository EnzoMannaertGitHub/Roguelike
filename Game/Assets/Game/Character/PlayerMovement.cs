using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D _controller;
    [SerializeField] private Animator _animator;

    private float _horizontalMove = 0f;
    private float _runSpeed = 5f;
    private bool _isRolling = false;
    private float _elapsedRollSec = 0;
    bool _IsJumping = false;
    bool _IsDoubleJumping = false;
    bool _canDoubleJump = false;
    public bool IsLeft;
    public bool IsMoving;
    private bool canMove = true;

    public void SetCanMove(bool state)
    {
        canMove = state;

        if (state == false)
        {
            _IsJumping = false;
        }
    }

    void Update()
    {
        if (!canMove)
        {
            _horizontalMove = 0f;
            return;
        }

        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
        float inputLength = Mathf.Abs(_horizontalMove);

        // Left check
        if (_horizontalMove < 0)
        {
            IsLeft = true;
        }
        else if (_horizontalMove > 0)
        {
            IsLeft = false;
        }

        // Move check
        IsMoving = (inputLength > 0);

        if (_controller._grounded)
        {
            _canDoubleJump = false;
        }

        // Jump check
        if (Input.GetButtonDown("Jump"))
        {
            _IsJumping = true;
            if (_IsJumping && _canDoubleJump)
            {
                _IsDoubleJumping = true;
                _canDoubleJump = false;
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            _IsJumping = false;
            _IsDoubleJumping = false;
            _canDoubleJump = true;
        }

        //Roll check
        if (Input.GetButtonDown("Roll") && !_isRolling)
        {
            _controller.Roll(IsLeft);
            _isRolling = true;
            _animator.SetTrigger("Roll");
        }

        // Animations
        UpdateAnimations(inputLength);
    }

    private void UpdateAnimations(float horizontalSpeed)
    {
        _animator.SetFloat("SpeedX", horizontalSpeed);
    }

    void FixedUpdate()
    {
        if (_isRolling)
        {
            _elapsedRollSec += Time.deltaTime;
            if (_elapsedRollSec >= 0.5f)
            {
                _isRolling = false;
                _elapsedRollSec = 0;
            }
        }
        else
            _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _IsJumping, _IsDoubleJumping);
    }

    public void HandleKnockBack(Vector2 direction)
    {
        _controller.Knockback(direction);
    }
}