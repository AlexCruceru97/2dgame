using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : NPC,IInteractable
{
    [SerializeField]
    private VendorItem[] items;

    public VendorItem[] MyItems { get => items;  }

    //public bool IsOpen { get; set; }

    //[SerializeField]
    //private VendorWindow vendorWindow;
    //public void Interact()
    //{
    //    if (!IsOpen)
    //    {
    //        IsOpen = true;
    //        vendorWindow.CreatePages(items);
    //        vendorWindow.Open(this);
    //    }
    //}





}
