using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootWindow : MonoBehaviour
{
    private static LootWindow instance;

    [SerializeField]
    private LootButton[] lootButtons;

    private List<List<Item>> pages = new List<List<Item>>();

    private List<Item> droppedLoot = new List<Item>();

    private CanvasGroup canvasGroup;

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private GameObject nextBtn, previousBtn;


    
    //debug only
    [SerializeField]
    private Item[] items;

    public static LootWindow MyInstance 
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LootWindow>();
            }
            return instance;
        }
    }
    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    // Start is called before the first frame update
   

    public void CreatePages(List<Item> items)
    {
        if (!IsOpen)
        {
            List<Item> page = new List<Item>();

            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 4 || i == items.Count - 1)
                {
                    pages.Add(page);
                    //create new page
                    page = new List<Item>();
                }
            }
            AddLoot();
            Open();
        }
       
    }

   private void AddLoot()
    {
        if (pages.Count > 0) 
        {
            //handle page numbers
            pageNumber.text = pageIndex + 1 + "/" + pages.Count;

            //handle next and previous buttons
            previousBtn.SetActive(pageIndex > 0);
            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);//dont show if it is on the last page

            for (int i = 0; i < pages[pageIndex].Count; i++)
            {

                if (pages[pageIndex][i] != null)
                {
                    //set the loot buttons icon
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;

                    lootButtons[i].MyLoot = pages[pageIndex][i];

                    //make sure the loot button is visible
                    lootButtons[i].gameObject.SetActive(true);
                    //make sure that the title is correct
                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);
                    lootButtons[i].MyTitle.text = title;
                }
               
            }
        }

    }

    public void ClearButtons()
    {
        foreach(LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        //check if it has more pages
        if (pageIndex < pages.Count - 1)
        {
            pageIndex++;
            ClearButtons();
            AddLoot();
        }
    }

    public void PreviousPage()
    {
        //check if it has more pages in the backward direction
        if (pageIndex > 0)
        {
            pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);
        droppedLoot.Remove(loot);
        //also remove from loot table

        if (pages[pageIndex].Count == 0)
        {
            //remove the empty page
            pages.Remove(pages[pageIndex]);
            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }
            AddLoot();
        }
    }

    public void Close()
    {
        pages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButtons();
    }

    
    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
