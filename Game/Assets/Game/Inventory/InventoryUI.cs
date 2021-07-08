using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory _inventory;
    InventorySlot[] _slots;
    [SerializeField] private GameObject _inventoryUI;

    public Transform Itemsparent;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.onItemChangedCallback += UpdateUI;

        _slots = Itemsparent.GetComponentsInChildren<InventorySlot>();
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
