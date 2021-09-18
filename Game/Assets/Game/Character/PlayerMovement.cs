using UnityEngine; 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _runSpeed = 2f;
    [SerializeField] private float _maxRunSpeed = 6f;
    [SerializeField] private float _runReduce = 3f;

    private float _horizontalMove = 0f;
    private float _elapsedRollSec = 0;
    bool _IsJumping = false;
    bool _IsDoubleJumping = false;
    bool _canDoubleJump = false;
    public bool IsLeft;
    public bool IsMoving;
    public bool IsRolling = false;
    private bool canMove = true;
    private bool _isEnteringCave = false;
    private bool _isRunning = false;
    public bool IsEnteringCave { get { return _isEnteringCave; } set { _isEnteringCave = value; } }

    public void SetCanMove(bool state)
    {
        canMove = state;

        if (state == false)
        {
            _IsJumping = false;
        }
    }

    public bool GetGroundState()
    {
        return _controller._grounded;

    }

    void Update()
    {
        if (!canMove)
        {
            _horizontalMove = 0f;
            return;
        }
        var axisFloat = Input.GetAxisRaw("Horizontal");
        Debug.Log(_isRunning);
        if(axisFloat != 0)
        {
            if (Mathf.Abs(_horizontalMove) < ((_isRunning ? _maxRunSpeed * 2 : _maxRunSpeed)))
                _horizontalMove += axisFloat * ((_isRunning ? _runSpeed * 2: _runSpeed) * Time.deltaTime);
            else
                _horizontalMove = axisFloat * _maxRunSpeed;
        }
        else if(axisFloat == 0)
        {
            if (_horizontalMove > 1 || _horizontalMove < -1)
                _horizontalMove -= Mathf.Sign(_horizontalMove) * (_runReduce * Time.deltaTime);
            else
                _horizontalMove = 0;             
        }
        //if going opposite direction
        if(Mathf.Sign(_horizontalMove) != Mathf.Sign(axisFloat))
        {
            _horizontalMove -= Mathf.Sign(_horizontalMove) * (_runReduce * Time.deltaTime);
        }

        

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
        //run check
        if (Input.GetButtonDown("Run"))
        {
            _isRunning = true;
        }
        else if (Input.GetButtonUp("Run"))
        {
            _isRunning = false;

        }

        //Roll check
        if (Input.GetButtonDown("Roll") && !IsRolling)
        {
            _controller.Roll(IsLeft);
            IsRolling = true;
            _animator.SetTrigger("Roll");
        }

        // Animations
        UpdateAnimations(inputLength);
    }

    private void UpdateAnimations(float horizontalSpeed)
    {
        if (IsEnteringCave)
        {
            _animator.SetFloat("SpeedX", _runSpeed);
            return;
        }
            _animator.SetFloat("SpeedX", horizontalSpeed);
    }

    void FixedUpdate()
    {
        if (IsEnteringCave)
        {
            if (Mathf.Abs(_horizontalMove) > 1)
                _controller.Move(_horizontalMove * Time.fixedDeltaTime, false, _IsJumping, _IsDoubleJumping);
            else
                _controller.Move(5 * Time.fixedDeltaTime, false, _IsJumping, _IsDoubleJumping);
            return;
        }

        if (IsRolling)
        {
            _elapsedRollSec += Time.deltaTime;
            if (_elapsedRollSec >= 0.5f)
            {
                IsRolling = false;
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