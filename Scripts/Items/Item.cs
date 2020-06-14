using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : ScriptableObject,IMoveable,IDescribeable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    [SerializeField]
    private int stackSize;

    private SlotScript slot;

    private CharButton charButton;

    [SerializeField]
    private int price;

   

    public Sprite MyIcon { get => icon; }

    public int MyStackSize { get => stackSize; }

    public SlotScript MySlot { get => slot; set => slot = value; }
    public Quality MyQuality { get => quality;  }
    public string MyTitle { get => title; }
    public CharButton MyCharButton
    {
        get
        { 
            return charButton;
        }
        set
        {
            MySlot = null;
            charButton = value;
        }
    }

    public int MyPrice { get => price;  }

    public virtual string GetDescription()
    {
        

      
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality], MyTitle);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
            
        }

    }

}
