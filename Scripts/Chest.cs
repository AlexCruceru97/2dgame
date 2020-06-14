﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openSprite, closedSprite;

    private bool isOpen=false;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private BagScript bag;

    private List<Item> items;
    public void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            AddItems();
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void StopInteract()
    {
        if (isOpen)
        {
            StoreItems();
            bag.Clear();
            isOpen = false;
            spriteRenderer.sprite = closedSprite;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
        
    }

    public void AddItems()
    {
        if (items != null)
        {
            foreach (Item item in items)
            {
                item.MySlot.AddItem(item);
            }
        }
    }

    public void StoreItems()
    {
        items = bag.GetItems();
    }

}