using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : MonoBehaviour,IComparable<Obstacle>
{

    public SpriteRenderer MySpriteRenderer { get; set; }

    private Color fadedColor;

    private Color defaultColor;
    

    //used for sorting obstacles so that we can pick the correct obstacle;
    public int CompareTo(Obstacle other)
    {
        if (MySpriteRenderer.sortingOrder > other.MySpriteRenderer.sortingOrder)
        {
            return 1;//if this obstacles has a higher sort order
        }
        else if (MySpriteRenderer.sortingOrder < other.MySpriteRenderer.sortingOrder)
        {
            return -1;//if this obstacles has a lower sort order
        }
        return 0;//if both obstacles have an equal sort order
    }

    // Start is called before the first frame update
    void Start()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();

        defaultColor = MySpriteRenderer.color;
        fadedColor = defaultColor;
        fadedColor.a = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        MySpriteRenderer.color = fadedColor;
    }

    public void FadeIn()
    {
        MySpriteRenderer.color = defaultColor;
    }
}
