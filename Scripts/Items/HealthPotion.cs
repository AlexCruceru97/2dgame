using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HealthPotion",menuName ="Item/Potion",order =2)]
public class HealthPotion : Item,IUseable
{
    [SerializeField]
    private int health;
    public void Use()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue < Player.MyInstance.MyHealth.MyMaxValue)
        {
            Remove();
            Player.MyInstance.MyHealth.MyCurrentValue += health;
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription()+ string.Format("\n<color=#00ff00ff>A potion that restores {0} health points</color>",health);
    }

}
