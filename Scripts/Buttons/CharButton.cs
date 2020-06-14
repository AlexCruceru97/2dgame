using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private ArmorType armorType;


    private Armor equippedArmor;
    [SerializeField]
    private Image slotIcon;
    [SerializeField]
    private Image icon;

    [SerializeField]
    private GearSocket gearSocket;

    [SerializeField]
    private WeaponSocket weaponSocket;////////////////////

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //equip armor
            if(HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor currentArmor = (Armor)HandScript.MyInstance.MyMoveable;
                if (currentArmor.MyArmorType==armorType)
                {
                    EquipArmor(currentArmor);//current armor =tmp
                    
                }
                UIManager.MyInstance.RefreshToolTip(currentArmor);
            }
        }
        else if (HandScript.MyInstance.MyMoveable == null && equippedArmor != null) 
        {
            HandScript.MyInstance.TakeMoveable(equippedArmor);
            CharacterPanel.MyInstance.MySelectedButton = this;
            icon.color = Color.grey;
        }

    }

    public void EquipArmor(Armor armor)
    {
        armor.Remove();

        if (equippedArmor != null)
        {
            if (equippedArmor != armor)
            {
                
                    //swqap armor
                    armor.MySlot.AddItem(equippedArmor);
                
              
            }
            
            UIManager.MyInstance.RefreshToolTip(equippedArmor);
        }
        else
        {
            UIManager.MyInstance.HideTooltip();
        }
        slotIcon.enabled = false;//or delete it for error.
        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;
        this.equippedArmor = armor;//reference to the equipped armor
        this.equippedArmor.MyCharButton = this;

        if(HandScript.MyInstance.MyMoveable==(armor as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }

        if (gearSocket != null && equippedArmor.MyAnimationClips != null) 
        {
            gearSocket.Equip(equippedArmor.MyAnimationClips);
        }
        //weapong equip
        if (weaponSocket != null && equippedArmor.MyAnimationClips != null)////////////////////
        {
            weaponSocket.EquipWeapon(equippedArmor.MyAnimationClips);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedArmor != null)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(0, 0), transform.position,equippedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        slotIcon.enabled = true;
        icon.enabled = false;
   

       //no more animation for armor
        if (gearSocket != null && equippedArmor.MyAnimationClips != null)
        {
            gearSocket.Dequip();
        }
        equippedArmor.MyCharButton = null;
        equippedArmor = null;
    }
}
