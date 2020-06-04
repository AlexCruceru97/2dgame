using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);
public delegate void CharacterRemoved();
public class NPC : Character
{
    public event CharacterRemoved characterRemoved;
    public event HealthChanged healthChanged;
    public virtual void DeSelect()
    {

    }

    public virtual Transform Select()
    {
        return hitbox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }
    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }
        Destroy(gameObject);
    }
}
