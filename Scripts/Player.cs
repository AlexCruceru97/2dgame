﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    [SerializeField]
    private float initHealth = 100;

    [SerializeField]
    private float initMana = 50;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health.Initialize(initHealth, initHealth);//to have full health when the game start
        mana.Initialize(initMana, initMana);//to have full mana when the game start
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        base.Update();
       
    }
   

    private void GetInput() {

        direction = Vector2.zero;

        //Used forDebugging only
        ///
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }
        /////
        //////
        ///


        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        
    }

}
