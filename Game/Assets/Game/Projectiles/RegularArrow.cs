public class RegularArrow : Projectile
{
    protected override void Shoot()
    {
        _rigidBody.velocity = _direction * _speed;
    }
}