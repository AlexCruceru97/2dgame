using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    private static CharacterPanel instance;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private CharButton head, chest, hands, legs, feet, weapon;

    [SerializeField]
    private Text gold;

    public CharButton MySelectedButton { get; set; }

    public static CharacterPanel MyInstance 
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterPanel>();
            }
            return instance;
        }
    }

    public Text MyGold { get => gold; set => gold = value; }

    /// <summary>
    /// /
    /// </summary>

   
    public void GetGold()
    {
        float currentGold = Player.MyInstance.MyGold;
        if (currentGold > 10000)
        {
            MyGold.text = (currentGold/10000).ToString() + " $";

        }
        else if (currentGold > 100)
        {
            MyGold.text = (currentGold / 100).ToString() + " Gold";
        }
        else if (currentGold >= 0)
        {
            MyGold.text = currentGold.ToString() + " Silver";
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }
    public void EquipArmor(Armor armor)
    {
        switch (armor.MyArmorType)
        {
            case ArmorType.Helmet:
                head.EquipArmor(armor);
                break;
            case ArmorType.Chest:
                chest.EquipArmor(armor);
                break;
            case ArmorType.Glove:
                hands.EquipArmor(armor);
                break;
            case ArmorType.Legs:
                legs.EquipArmor(armor); 
                break;
            case ArmorType.Boots:
                feet.EquipArmor(armor);
                break;
            case ArmorType.Weapon:
                weapon.EquipArmor(armor);
                break;
        }
    }
}
