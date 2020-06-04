using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    

    [SerializeField]
    private Stat mana;

    

    [SerializeField]
    private float initMana = 50;

    [SerializeField]
    private Block[] blocks;

    private int exitIndex=2;

    //reference to spellbook
    private SpellBook spellBook;
    //Player Target
    //
   


    private Vector3 min, max;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //health.Initialize(initHealth, initHealth);//to have full health when the game start
        mana.Initialize(initMana, initMana);//to have full mana when the game start
        spellBook = GetComponent<SpellBook>();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y),transform.position.z);
        base.Update();
       
    }
   

    private void GetInput() {

        Direction = Vector2.zero;

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
       //////////////////////////////////////////

        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            Direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            Direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            Direction += Vector2.right;
        }

        if (isMoving)
        {
            StopAttack();
        }

        //attack key
        if (Input.GetKeyDown(KeyCode.Space))
        {



            if (!IsAttacking && !isMoving) ///for warrior i may take inlineofsight out
            {

                attackRoutine = StartCoroutine(Atack());
            }
        }
        
    }


    public void SetLimits(Vector3 min,Vector3 max)
    {
        this.min = min;
        this.max = max;
    }


    /*
     * attack  
     * 
     * 
     * **************************************
     */


    private IEnumerator Atack()
    {


        IsAttacking = true;

        MyAnimator.SetBool("attack", IsAttacking);//when i start attacking will set it true

        yield return new WaitForSeconds(1);//this is hardcoded cast time for debugging

        StopAttack();

    }

    private IEnumerator AtackSpell(int spellIndex)
    {

        Transform currentTarget = MyTarget;
        Spell newSpell = spellBook.CastSpell(spellIndex);
            
        IsAttacking = true;
            
        MyAnimator.SetBool("attack", IsAttacking);//when i start attacking will set it true

        yield return new WaitForSeconds(newSpell.MyCastTime);//this is hardcoded cast time for debugging


        //after i cast my spell and try to change to a target it wont change atack that target with the current spell
        if (currentTarget!= null && InLineOfSight())
        {

            SpellScript s= Instantiate(newSpell.MySpellPrefab, transform.position, Quaternion.identity).GetComponent<SpellScript>() ;
            s.Initialize(currentTarget,newSpell.MyDamage,transform);
        }
             


            StopAttack();
        
    }


    

    /// <summary>
    /// Spell atack
    /// </summary>
    //[SerializeField]
    //private GameObject[] spellPrefab;

    
    public void CastSpell(int spellIndex)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !isMoving && InLineOfSight()) ///for warrior i may take inlineofsight out
        {

            attackRoutine = StartCoroutine(AtackSpell(spellIndex));
        }
       
       
    }

    ///
    ///Check player line of sight. perhaps use it to warrior too
    ///

    

    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {

        //calculate target direction
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

       // MyTarget = GameObject.Find("Target").transform;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.position),256);

        if (hit.collider == null)
        {
            return true;
        }

        }
        return false;

    }

    //For Line of Sight
    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();

    }

    //public override void StopAttack()
    //{

    //    spellBook.StopCasting();
    //    base.StopAttack();
    //}
    //stop the attack
    public  void StopAttack()
    {
        spellBook.StopCasting();
        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking);
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);

        }
    }
}
