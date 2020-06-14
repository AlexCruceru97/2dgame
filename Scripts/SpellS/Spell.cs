using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
 public class Spell :IUseable,IMoveable,IDescribeable
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private string description;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private Color barColor;

    public string MyName { get => name;  }
    public int MyDamage { get => damage;  }
    public Sprite MyIcon { get => icon;  }
    public float MyCastTime { get => castTime;  }
    public float MySpeed { get => speed;  }
    public GameObject MySpellPrefab { get => spellPrefab;  }
    public Color MyBarColor { get => barColor;  }

    public string GetDescription()
    {
        return string.Format("{0}\nCast time:{1} second(s)\n<color=#ffd111>{2} at your enemy\nthat causes {3} damage</color>", name,castTime,description,damage);
    }

    public void Use()
    {
        Player.MyInstance.CastSpell(MyName);
    }
}
