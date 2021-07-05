using UnityEngine;

public class pickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up" + item.name);
        bool wasPickedUp =  Inventory.Instance.Additem(item);
        if(wasPickedUp)
            Destroy(gameObject);
    }
}