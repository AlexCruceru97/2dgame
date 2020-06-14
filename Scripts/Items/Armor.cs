using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmorType { Helmet,Chest,Arms,Glove,Legs,Boots,Weapon}
[CreateAssetMenu(fileName = "Armor", menuName = "Item/Armor", order = 3)]
public class Armor :Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private int intelligence;
    [SerializeField]
    private int strength;
    [SerializeField]
    private int stamina;

    //AAAAAAAAAAAAAAA
    [SerializeField]
    private string sex;
    public string MySex { get => sex; }

    [SerializeField]
    private AnimationClip[] animationClips;

    internal ArmorType MyArmorType { get => armorType;  }

    public AnimationClip[] MyAnimationClips { get => animationClips; }

    public override string GetDescription()
    {
        string stats = string.Empty;
        if (stamina > 0)
        {
            stats += string.Format("\n+{0} stamina", stamina);
        }
        if (strength > 0)
        {
            stats += string.Format("\n+{0} strength", strength);
        }
        if (intelligence > 0)
        {
            stats += string.Format("\n+{0} intelligence", intelligence);
        }
        return base.GetDescription()+ "\n" + MySex  + stats;
    }

    public void Equip()
    {
        CharacterPanel.MyInstance.EquipArmor(this);
    }
}
