using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D _controller;

    private float _horizontalMove = 0f;
    private float _runSpeed = 5f;
    bool _IsJumping = false;

    public bool IsLeft;
    public bool IsMoving;

    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        // Left check
        IsLeft = (_horizontalMove > 0);

        // Move check
        IsMoving = (Mathf.Abs(_horizontalMove) > 0);

        // Jump check
        if (Input.GetButtonDown("Jump"))
        {
            _IsJumping = true;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            _IsJumping = false;
        }
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
