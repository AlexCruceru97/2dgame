using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour,IPointerClickHandler,IClickable, IPointerEnterHandler, IPointerExitHandler

{
    [SerializeField]
    private Text stackSize;
    private Stack<IUseable> useables = new Stack<IUseable>();
    private int count;
    public IUseable MyUseable { get; set; }
    public Button MyButton { get; private set; }
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
            return count;
        }
    }

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public Stack<IUseable> MyUseables
    {
        get
        {
            return useables;
        }
        set
        {
            if (value.Count > 0)
            {
                MyUseable = value.Peek();
            }
            else
            {
                MyUseable = null;
            }
           
            useables = value;
        }
    }

    [SerializeField]
    private Image icon;

    // or awake 
   private void Start()
    {
        MyButton = GetComponent<Button>();

        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            else if(MyUseables!=null && MyUseables.Count > 0)
            {
                MyUseables.Peek().Use();
            }
        }
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
            }
        }
    }
    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            MyUseables = InventoryScript.MyInstance.GetUseables(useable);
            count = MyUseables.Count;
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
        }
        else{

            MyUseables.Clear();
            this.MyUseable = useable;
        }
        count = MyUseables.Count;
        UpdateVisual();
        UIManager.MyInstance.RefreshToolTip(MyUseable as IDescribeable);

    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;
        //display it only if it is>1
        if (count > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
        else if(MyUseable is Spell)
        {
            UIManager.MyInstance.ClearStackCount(this);
        }
    }
    public void UpdateItemCount(Item item)
    {
        //if the item is the same as we have on this button
        if(item is IUseable && MyUseables.Count > 0)
        {
            if (MyUseables.Peek().GetType() == item.GetType())
            {
                MyUseables = InventoryScript.MyInstance.GetUseables(item as IUseable);
                count = MyUseables.Count;
                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        IDescribeable tmp = null;
        if (MyUseable != null && MyUseable is IDescribeable)
        {
            tmp =(IDescribeable) MyUseable;
           // UIManager.MyInstance.ShowTootip(transform.position);
        }
        else if (MyUseables.Count > 0)
        {
           // UIManager.MyInstance.ShowTootip(transform.position);
        }
        if (tmp != null)
        {
            
            UIManager.MyInstance.ShowTooltip(new Vector2(0,0), transform.position, tmp);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
