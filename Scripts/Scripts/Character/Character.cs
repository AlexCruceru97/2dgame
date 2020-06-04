using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    //protected Animator MyAnimator;
    public Animator MyAnimator { get; set; }


    private Rigidbody2D myRigidBody;

    public Transform MyTarget { get; set; }

    //protected bool IsAttacking = false;

    public bool IsAttacking { get; set; }

    private Vector2 direction;

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
            return Direction.x != 0 || Direction.y != 0;
           
        }
    }

    public Vector2 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }

   public bool IsAlive
    {
        get
        {
           return health.MyCurrentValue > 0;
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        health.Initialize(initHealth, initHealth);//to have full health when the game start
        myRigidBody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
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
        if (IsAlive)
        {
            //transform.Translate(direction.normalized * speed * Time.deltaTime);
            //make sure that the player moves
            myRigidBody.velocity = Direction.normalized * Speed;
        }
    }

    public void HandleLayers()
    {
        if (IsAlive)
        {
            if (isMoving)
            {
                //Animate player movement

                ActivateLayer("WalkLayer");

                //Sets the animantion parameters so that it faces the correct direction
                MyAnimator.SetFloat("x", Direction.x);
                MyAnimator.SetFloat("y", Direction.y);
                //StopAttack();
            }
            else if (IsAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
            {
                //if there is no key pressed it goes into iddle animation
                ActivateLayer("IdleLayer");
            }
        }
        else
        {
            ActivateLayer("DeathLayer");
        }
    }


    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    ////stop the attack
    //public virtual void StopAttack()
    //{
    //    if (attackRoutine != null)
    //    {
    //        StopCoroutine(attackRoutine);
    //        isAttacking = false;
    //        myAnimator.SetBool("attack", isAttacking);

    //    }
    //}

    public virtual void TakeDamage(float damage,Transform source)
    {
        

        // health reduce
        health.MyCurrentValue -= damage;
        if (health.MyCurrentValue <= 0)
        {
            Direction = Vector2.zero;
            myRigidBody.velocity = Direction;
            //Die
            MyAnimator.SetTrigger("die");
        }
    }


}
