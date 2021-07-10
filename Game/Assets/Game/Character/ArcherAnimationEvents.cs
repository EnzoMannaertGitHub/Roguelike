using UnityEngine;

public class ArcherAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack = null;

    public void OnShoot()
    {
        _playerAttack.ShootArrow();
    }
}
