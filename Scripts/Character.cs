using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed=5;
    protected Animator myAnimator;
    private Rigidbody2D myRigidBody;
    protected bool isAttacking = false;
    protected Vector2 direction;

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitbox;

    [SerializeField]
    private float initHealth;

    [SerializeField]
    protected Stat health;

    //checks if the character is moving or not
    public bool isMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health.Initialize(initHealth, initHealth);//to have full health when the game start
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is marked as virtual so that we can override it in the subclasses
    protected virtual void Update()
    {
        HandleLayers();
    }


    //use when manipulate rigidbody
    private void FixedUpdate()
   
    {
        //Moves the character
        Move();
    }

    public void Move()
    
    {
        //transform.Translate(direction.normalized * speed * Time.deltaTime);
        //make sure that the player moves
        myRigidBody.velocity = direction.normalized * speed;      
    }

    public void HandleLayers()
    {
        if (isMoving)
        {
            //Animate player movement

            ActivateLayer("WalkLayer");

            //Sets the animantion parameters so that it faces the correct direction
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
            StopAttack();
        }
        else if (isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            //if there is no key pressed it goes into iddle animation
            ActivateLayer("IdleLayer");
        }
    }

   

    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    //stop the attack
    public virtual void StopAttack()
    {
        if (attackRoutine != null)
        {
        StopCoroutine(attackRoutine);
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);

        }
    }

    public virtual void TakeDamage(float damage)
    {
        // health reduce
        health.MyCurrentValue -= damage;
        if (health.MyCurrentValue <= 0)
        {
            //Die
            myAnimator.SetTrigger("die");
        }
    }


}
