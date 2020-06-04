using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    //private Transform target;

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
    public override Transform Select()
    {
        healthGroup.alpha = 1;
        //perhaps put first
        return base.Select();
    }

    //to deselect the enemy
    public override void DeSelect()
    {

        healthGroup.alpha = 0;
        base.DeSelect();
    }

    public override void TakeDamage(float damage,Transform source)
    {
        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);
            OnHealthChanged(health.MyCurrentValue);
        }
        
    }

    //private void FollowTarget()
    //{
    //    if (target != null)
    //    {
    //        //direction = (target.transform.position - transform.position).normalized;
    //        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        direction = Vector2.zero;
    //    }
    //}

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

    public void Reset()
    {
        MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.health.MyCurrentValue = this.health.MyMaxValue;
        OnHealthChanged(health.MyCurrentValue);
    }
}
