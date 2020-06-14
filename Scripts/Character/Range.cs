using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{

    private Enemy parent;

    private void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    //when we enter enemy range it will follow us
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            parent.SetTarget(collision.transform);
        }
    }

    //when we leave his range it will stop following
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        parent.MyTarget = null;
    //    }
    //}
}
