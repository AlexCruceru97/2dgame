using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{


    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float speed;

   

    public Transform MyTarget { get; private set; }

    private int damage;
    
    void Start()
    {
        //creates rigid body for spell
        myRigidBody = GetComponent<Rigidbody2D>();

    }

    public void Initialize(Transform target, int damage)
    {
        this.MyTarget = target;
        this.damage = damage;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (MyTarget!=null)
        {

            //calculates spell direction
            Vector2 direction = MyTarget.position - transform.position;

            //moves the spell by using rigid body
            myRigidBody.velocity = direction.normalized * speed;

            //the angle of spell
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //rotates the spell towards the target
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox" && collision.transform==MyTarget)
        {
            speed = 0;
            collision.GetComponentInParent<Enemy>().TakeDamage(damage);
            GetComponent<Animator>().SetTrigger("impact");
            myRigidBody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}
