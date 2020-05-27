﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
 public class Spell 
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

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
}
