using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LootButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    private LootWindow lootWindow;

    public Image MyIcon { get => icon;  }
    public Text MyTitle { get => title;  }

    public Item MyLoot
    {
        get;set;
    }

    private void Awake()
    {
        lootWindow = GetComponentInParent<LootWindow>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ////loot items;
        //if (InventoryScript.MyInstance.AddItem(MyLoot))
        //{
        //    gameObject.SetActive(false);
        //    lootWindow.TakeLoot(MyLoot);
        //    UIManager.MyInstance.HideTooltip();
        //}
        //Here is where we need to first instantiate the item to make it unique
        Item loot = (Item)Instantiate(MyLoot);
        //And here we loot the Instantiated version of the item
        if (InventoryScript.MyInstance.AddItem(loot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(MyLoot);
            UIManager.MyInstance.HideTooltip();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(new Vector2(1,0), transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
