using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory _inventory;
    InventorySlot[] _slots;
    bool _itemPickedUp = false;
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private GameObject _itemUI;

    public Transform Itemsparent;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.onItemChangedCallback += UpdateUI;

        _slots = Itemsparent.GetComponentsInChildren<InventorySlot>();
    }

    public void PickedUpItem(string name)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory._items.Count)
            {
                _slots[i].AddItem(_inventory._items[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}
