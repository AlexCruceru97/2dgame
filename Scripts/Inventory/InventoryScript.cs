﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);
public class InventoryScript : MonoBehaviour
{

    public event ItemCountChanged itemCountChangedEvent;
    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach(Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }
            return count;
        }
    }
    //how many slots has the bag
    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }
            return count;
         }
    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();
    [SerializeField]
    private BagButton[] bagButtons;
    //debugging
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get { return bags.Count < 5; }
    }

    public SlotScript FromSlot 
    {
        get {
            return fromSlot; 
        }
        set { 
           
            fromSlot = value;
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
            
        }
    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(16);
        bag.Use();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {
            //Bag bag = (Bag)Instantiate(items[0]);
            //bag.Initialize(8);
            //AddItem(bag);
            AddItem((Bag)Instantiate(items[14]));
            AddItem((Bag)Instantiate(items[13]));
        }

      
        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(16);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion potion =(HealthPotion) Instantiate(items[1]);
            AddItem(potion);
            
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem((Armor)Instantiate(items[2]));
            AddItem((Armor)Instantiate(items[3]));
            AddItem((Armor)Instantiate(items[4]));
            AddItem((Armor)Instantiate(items[5]));
            AddItem((Armor)Instantiate(items[6]));
            AddItem((Armor)Instantiate(items[7]));
            AddItem((Armor)Instantiate(items[8]));
            AddItem((Armor)Instantiate(items[9]));
            AddItem((Armor)Instantiate(items[10]));
            AddItem((Armor)Instantiate(items[11]));
            AddItem((Armor)Instantiate(items[12]));
        }
    }

    //equip a bag to the inventory
    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);
                bag.MyBagButton = bagButton;
                //set the bag correct in hierarchy
                bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
                break;
            }
        }
    }

    public void AddBag(Bag bag, BagButton bagButton)
    {
        bags.Add(bag);
        bagButton.MyBag = bag;
        //set the bag correct inhierarchy
        bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
    }

    public void RemoveBag(Bag bag)
    {
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    //swap the bags
    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCount = (MyTotalSlotCount - oldBag.MySlots) + newBag.MySlots;

        if (newSlotCount - MyFullSlotCount >= 0)
        {
            //Swap
            List<Item> bagItems = oldBag.MyBagScript.GetItems();
            RemoveBag(oldBag);

            newBag.MyBagButton = oldBag.MyBagButton;
            newBag.Use();
            foreach(Item item in bagItems)
            {
                if (item != newBag)
                {
                    AddItem(item);//make sure we dont get a duplicate bag
                }
            }
            AddItem(oldBag);
            HandScript.MyInstance.Drop();
            MyInstance.fromSlot = null;
        }
    }

    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }
        return PlaceInEmpty(item);
    }

    private bool PlaceInEmpty(Item item)
    {
        foreach(Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        //inventory is full
        return false;
    }

    private bool  PlaceInStack(Item item)
    {
        foreach (Bag bag in bags)
        {
            foreach(SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;
    }

    public void OpenClose()
    {
        bool closeBag = bags.Find(x => !x.MyBagScript.IsOpen);
        //if closebag==true then open all closed bags else open bags

        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.IsOpen != closeBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();
        foreach(Bag  bag in bags)
        {
            foreach(SlotScript slot in bag.MyBagScript.MySlots)
            {
                if(!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach(Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }
        return useables;
    }

    public void OnItemCountChanged(Item item)
    {
        if(itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
    
    public int GetItemCount(string type)
    {
        int itemCount = 0;
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slot in bag.MyBagScript.MySlots)
            {
                if(!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    itemCount += slot.MyItems.Count;
                }
            }
        }
        return itemCount;
    }
}
