using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    //references to the buttons
    [SerializeField]
    private ActionButton[] actionButtons;

   
    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private RectTransform tooltipRect;

    private Text tooltipText;
    [SerializeField]
    private CharacterPanel charPanel;
    private static UIManager instance;
    
    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private CanvasGroup keybindMenu;

    [SerializeField]
    private CanvasGroup spellBook;

    private GameObject[] keybindButtons;


    private void Awake()
    {
        keybindButtons= GameObject.FindGameObjectsWithTag("KeyBind");
        tooltipText = tooltip.GetComponentInChildren<Text>();
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(keybindMenu);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenClose(spellBook);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            charPanel.OpenClose();
        }
        charPanel.GetGold();
    }


  


    /// <summary>
    ///updates the text on a keybindbutton after the key has been changed
    /// </summary>
    /// <param name="key"></param>
    /// <param name="code"></param>
    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons,x=>x.gameObject.name==buttonName).MyButton.onClick.Invoke();
    }

    
    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;//if keybind.alpha is larger than 0 put it to zero else put it to 1
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
        
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);//when the stack goes from 2 to 1
            clickable.MyIcon.color = Color.white;

        }
        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }

    }

    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyStackText.color = new Color(0, 0, 0, 0);//when the stack goes from 2 to 1
        clickable.MyIcon.color = Color.white;
    }

    public void ShowTooltip(Vector2 pivot, Vector3 position,IDescribeable description)
    {
        tooltipRect.pivot = pivot;
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        tooltipText.text = description.GetDescription();
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
    public void RefreshToolTip(IDescribeable description)
    {
        tooltipText.text = description.GetDescription();
    }
}
