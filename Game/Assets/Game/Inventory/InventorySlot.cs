using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    Item _item;
    [SerializeField] Image icon;
    [SerializeField] Button _removebutton;
    public void AddItem(Item newitem)
    {
        _item = newitem;
        icon.sprite = _item.icon;
        icon.enabled = true;
        _removebutton.interactable = true;
        newitem.Use();
    }

    public void ClearSlot()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        _removebutton.interactable = false;
    }

    public void OnremoveButton()
    {
        _item.OnClear();
        Inventory.Instance.removeItem(_item);
    }
}
