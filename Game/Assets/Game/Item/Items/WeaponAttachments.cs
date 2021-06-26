using UnityEngine;
[CreateAssetMenu(fileName = "New Attachment",menuName = "Inventory/Attachments")]
public class WeaponAttachments : Item
{
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;

    public override void Use()
    {
        Debug.Log("using " + name);
       var primaryWeapon = FindObjectOfType<PlayerAttack>();
       //primaryWeapon.ChangeDamage(_damage);
       //primaryWeapon.ChangeFirerate(-_fireRate);
       //primaryWeapon.ChangeRange(_range);
       //primaryWeapon.ChangeSpeed(_bulletSpeed);
       base.Use();
    }

    public override void OnClear()
    {
        Debug.Log("Clearing " + name);
        var primaryWeapon = FindObjectOfType<PlayerAttack>();
        //primaryWeapon.ChangeDamage(-_damage);
        //primaryWeapon.ChangeFireRate(_fireRate);
        //primaryWeapon.ChangeRange(-_range);
        //primaryWeapon.ChangeSpeed(-_bulletSpeed);
        base.Use();
    }
}
