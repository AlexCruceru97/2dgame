using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed=5;
    private Animator animator;
    protected Vector2 direction;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Moves the character
        Move();
    }
    public void Move()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0){
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
        
    }

    public void AnimateMovement(Vector2 direction)
    {
        //Set the layers weight so that it is visible
        animator.SetLayerWeight(1, 1);

        //Sets the animantion parameters so that it faces the correct direction
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

}
