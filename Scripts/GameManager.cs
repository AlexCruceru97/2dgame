﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Player player;

    private NPC currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ClickTarget();
       
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity,512);
            if (hit.collider != null)
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                currentTarget = hit.collider.GetComponent<NPC>();

                player.MyTarget = currentTarget.Select();
            }
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                currentTarget = null;
                player.MyTarget = null;
            }
            
            //{
            //    if (hit.collider.tag == "Enemy")
                   
            //        player.MyTarget = hit.transform.GetChild(0);
            //}
            //else
            //{
               
                
            //player.MyTarget = null;
            //}
        }
    }


}
