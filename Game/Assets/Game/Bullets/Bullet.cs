using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float _speed = 10f;
    [SerializeField] protected float _range = 1.5f;

    [SerializeReference] protected Rigidbody2D _rigidBody;
    protected Camera _mainCam;

    protected Vector2 _direction;
    protected Vector2 _startPos;

    void Update()
    {
        Shoot();
        HandleRange();
    }
    protected abstract void Shoot();
    protected void HandleRange()
    {
        if (Vector3.Distance(transform.position, _startPos) >= _range)
        {
            Debug.Log("Destroy range " + _range);
            Destroy(gameObject);
            return;
        }
    }
    protected void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void SetRange(float range)
    {
        _range = range;
        Debug.Log("Setting range to " + _range);  
    }

    public void SetDirection(Vector2 direction, bool isMousePointer)
    {
        _mainCam = Camera.main;

        if (_mainCam == null) Debug.LogError("Bullet.cs: mainCam is null!");

        if (isMousePointer)
        {
            var mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 point = new Vector2(mousePos.x, mousePos.y);
            _startPos = new Vector2(transform.position.x, transform.position.y);
            _direction = (point - _startPos).normalized;

        }
        else
        {
            _startPos = new Vector2(transform.position.x, transform.position.y);
            _direction = direction;
        }

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);
    }

}