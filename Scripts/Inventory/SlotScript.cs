using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler,IPointerExitHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

    //reference to the bag this slot belongs to
    public BagScript MyBag { get; set; }

    public bool IsEmpty
    {
        get
        {
            return MyItems.Count == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            
           if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            return true;
        }
    }

    public Text MyStackText
    {
        get 
        { 
            return stackSize;
        }
    }

    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
            }
            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return MyItems.Count;
        }
    }

    public ObservableStack<Item> MyItems { get => items;  }

    private void Awake()
    {
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }
    public bool AddItem(Item item)
    {

        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;

        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;
            for(int i = 0; i < count; i++)
            {
                if(IsFull)
                {
                    return false;
                }
                
                AddItem(newItems.Pop());
            }
            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            
            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }

    public void Clear()
    {
        int initCount = MyItems.Count;
        if (initCount > 0)
        {
            for (int i = 0; i < initCount; i++)
            {
                InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            }
           
            //MyItems.Clear();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty)//if we dont have anything to move
            {
                if (HandScript.MyInstance.MyMoveable!=null )
                {
                    if(HandScript.MyInstance.MyMoveable is Bag)
                    {
                        //check if the item we click is a bag so we can swap it
                        if (MyItem is Bag)
                        {
                            InventoryScript.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);

                        }
                    }
                    else if(HandScript.MyInstance.MyMoveable is Armor)
                    {
                        if(MyItem is Armor && (MyItem as Armor).MyArmorType==(HandScript.MyInstance.MyMoveable as Armor).MyArmorType)
                        {
                            (MyItem as Armor).Equip();
                            //UIManager.MyInstance.RefreshToolTip();
                            HandScript.MyInstance.Drop();
                        }
                    }
                   
                }
                else
                {
                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                InventoryScript.MyInstance.FromSlot = this;

                }
            }
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty )
            {
                if(HandScript.MyInstance.MyMoveable is Bag)
                {
                    //dequip bag from inventory
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;
                    //mate sure we cant dequip into itself and that we have enough space for the items from dequipping bag
                    if (bag.MyBagScript != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - bag.MySlots > 0)
                    {
                        AddItem(bag);
                        bag.MyBagButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                    }
                }
                else if(HandScript.MyInstance.MyMoveable is Armor)
                {
                    Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
                    CharacterPanel.MyInstance.MySelectedButton.DequipArmor();
                    AddItem(armor);
                    HandScript.MyInstance.Drop();
                    
                }
            }
            else if (InventoryScript.MyInstance.FromSlot != null)//if we have smth to move
            {
                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) || SwapItems(InventoryScript.MyInstance.FromSlot) || AddItems(InventoryScript.MyInstance.FromSlot.MyItems))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
           
        }
        //on right click use the item but not while carrying another item
        if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable==null)
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
        else if(MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }

    public bool StackItem(Item item)
    {
        if(!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
        {
            MyItems.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }

    private bool PutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }

    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize)
        {
            //copy all the items we need to swap from a
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);
            //clear slto a
            from.MyItems.Clear();
            //all items from b and copy them into a
            from.AddItems(MyItems);

            //clear b
            MyItems.Clear();

            //move from a to b
            AddItems(tmpFrom);

            return true;
        }
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            
            int free = MyItem.MyStackSize - MyCount;
            for(int i = 0; i < free; i++)
            {
                if (from.MyCount > 0)
                {
                    AddItem(from.MyItems.Pop());
                }
            }
            return true;
        }
        return false;


    }

    public void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //show tooltip
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(1, 0), transform.position,MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //hide tooltip
        UIManager.MyInstance.HideTooltip();
    }
}

