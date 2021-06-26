using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D _controller;
    [SerializeField] private Animator _animator;

    private float _horizontalMove = 0f;
    private float _runSpeed = 5f;
    bool _IsJumping = false;

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

        // Jump check
        if (Input.GetButtonDown("Jump"))
        {
            _IsJumping = true;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            _IsJumping = false;
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
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _IsJumping);
    }

    public void HandleKnockBack(Vector2 direction)
    {
        _controller.Knockback(direction);
    }
}
