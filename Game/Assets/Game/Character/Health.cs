using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private SpriteRenderer _playerSprite;
    private bool _isImmune = false;
    private float _imuneTime = 0f;
    private float _maxImuneTime = 2f;
    private float _drawTime = 0f;

    private void Update()
    {
        if(_isImmune)
        {
            _imuneTime += Time.deltaTime;
            _drawTime += Time.deltaTime;
            if (_drawTime > 0.1f)
            {
                _drawTime = 0;
                _playerSprite.enabled = !_playerSprite.isVisible;
            }

            if(_imuneTime >= _maxImuneTime)
            {
                _isImmune = false;
                _playerSprite.enabled = true;
                _drawTime = 0;
                _imuneTime = 0;
            }
        }
    }
    public void GetHit(float health, Vector2 knockbackDir)
    {
        if(!_isImmune)
        {
            _health -= health;
            _movement.HandleKnockBack(knockbackDir);
            _isImmune = true;
        }
    }
}
