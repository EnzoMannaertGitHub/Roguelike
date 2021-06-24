using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D _controller;

    private float _horizontalMove = 0f;
    private float _runSpeed = 5f;
    bool _IsJumping = false;

    public bool IsLeft;
    public bool IsMoving;


    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;




        //left check
        if (_horizontalMove > 0)
            IsLeft = false;
        else
            IsLeft = true;

        //move check
        if (Mathf.Abs(_horizontalMove) > 0)
            IsMoving = true;
        else
            IsMoving = false;

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
