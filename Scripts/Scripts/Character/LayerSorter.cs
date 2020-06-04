using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorter : MonoBehaviour
{

    private SpriteRenderer parentRenderer;
    private List<Obstacle> obstacles = new List<Obstacle>();
    // Start is called before the first frame update
    void Start()
    {
        parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the player hits an obstacle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            //create a reference to the obstacle;
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeOut();
            if (obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder-1<parentRenderer.sortingOrder)
            {

            parentRenderer.sortingOrder =o.MySpriteRenderer.sortingOrder -1 ;
            }
            //ads obstacle to a list so we can keep its track
            obstacles.Add(o);
        }

       

    }


    //if it stopped colliding with an obstacle
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        { 

            //create a reference to the obstacle
            Obstacle o = collision.GetComponent<Obstacle>();
            o.FadeIn();
            obstacles.Remove(o);
            if (obstacles.Count == 0)
            {

            parentRenderer.sortingOrder = 200;
            }
            else
            {
                obstacles.Sort();
                parentRenderer.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
            }
        }
    }
}
