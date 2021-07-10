using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] new string name = "New Item";
    [SerializeField] public Sprite icon = null;

    public virtual void Use()
    {
        //use item
    }
    public virtual void OnClear()
    {

    }
}