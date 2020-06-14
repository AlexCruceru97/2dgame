using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "Item/Bag", order = 1)]
public class Bag : Item, IUseable
{  
    
    [SerializeField]
    private int slots;

    [SerializeField]
    protected GameObject bagPrefab;

    public BagButton MyBagButton { get; set; }

    public BagScript MyBagScript { get; set; }
    public int MySlots { get => slots; }

    public void Initialize(int slots)
    {
        this.slots = slots;
    }

    public void Use()
    {
        if (InventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slots);
            if (MyBagButton == null)
            {
                InventoryScript.MyInstance.AddBag(this);
            }
            else
            {
                InventoryScript.MyInstance.AddBag(this,MyBagButton);
            }
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\nA bag that gives you {0} more slots! ", slots);
    }
}
