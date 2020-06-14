using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);
public delegate void CharacterRemoved();
public class Enemy : Character,IInteractable
{
    public event CharacterRemoved characterRemoved;
    public event HealthChanged healthChanged;
    [SerializeField]
    private CanvasGroup healthGroup;

    [SerializeField]
    private LootTable lootTable;

   public float MyAttackRange { get; set; }

    public Vector3 MyStartPosition { get; set; }

   public float MyAttackTime { get; set; }

    private IState currentState;
    public float MyAggroRange { get; set; }


    [SerializeField]
    private float initAggroRange;



    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }
    //public Transform Target {
    //    get
    //    {
    //        return target;
    //    }
    //    set
    //    {
    //        target = value;
    //    }
    //}

    protected void Awake()
    {
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
        MyAttackRange = 0.7f;
        ChangeState(new IdleState());
    }



    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }
            currentState.Update();
        }
            base.Update();
    }

    //when we click on enemy
    public  Transform Select()
    {
        healthGroup.alpha = 1;
        return hitbox;
    }

    //to deselect the enemy
    public  void DeSelect()
    {

        healthGroup.alpha = 0;
       
    }

    public override void TakeDamage(float damage,Transform source)
    {
        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);
            OnHealthChanged(MyHealth.MyCurrentValue);
        }
        
    }

    

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    public void SetTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }
    public void Reset()
    {
        MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue;
        OnHealthChanged(MyHealth.MyCurrentValue);
    }

    public  void Interact()
    {
        if (!IsAlive)
        {
            lootTable.ShowLoot();
        }
    }
    public void StopInteract()
    {
        LootWindow.MyInstance.Close();
    }

    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }
        Destroy(gameObject);
    }
}
