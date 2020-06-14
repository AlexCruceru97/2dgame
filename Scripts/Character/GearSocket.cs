using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{

    public Animator MyAnimator { get; set; }

    private SpriteRenderer spriteRenderer;

    private Animator parentAnimator;

    private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentAnimator = GetComponentInParent<Animator>();
        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);
        MyAnimator.runtimeAnimatorController = animatorOverrideController;
    }

  
    public void SetXandY(float x,float y)
    {
        //Sets the animantion parameters so that it faces the correct direction
        MyAnimator.SetFloat("x", x);
        MyAnimator.SetFloat("y", y);
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public void Equip(AnimationClip[] animations)
    {

        spriteRenderer.color = Color.white;

        animatorOverrideController["Female_Attack_Down"] = animations[0];
        animatorOverrideController["Female_Attack_Left"] = animations[1];
        animatorOverrideController["Female_Attack_Right"] = animations[2];
        animatorOverrideController["Female_Attack_Up"] = animations[3];

        animatorOverrideController["Female_Idle_Down"] = animations[4];
        animatorOverrideController["Female_Idle_Left"] = animations[5];
        animatorOverrideController["Female_Idle_Right"] = animations[6];
        animatorOverrideController["Female_Idle_Up"] = animations[7];


        animatorOverrideController["Female_Walk_Down"] = animations[8];
        animatorOverrideController["Female_Walk_Left"] = animations[9];
        animatorOverrideController["Female_Walk_Right"] = animations[10];
        animatorOverrideController["Female_Walk_Up"] = animations[11];
    }
    public void Dequip()
    {
        animatorOverrideController["Female_Attack_Down"] = null;
        animatorOverrideController["Female_Attack_Left"] = null;
        animatorOverrideController["Female_Attack_Right"] = null;
        animatorOverrideController["Female_Attack_Up"] = null;

        animatorOverrideController["Female_Idle_Down"] = null;
        animatorOverrideController["Female_Idle_Left"] = null;
        animatorOverrideController["Female_Idle_Right"] = null;
        animatorOverrideController["Female_Idle_Up"] = null;


        animatorOverrideController["Female_Walk_Down"] = null;
        animatorOverrideController["Female_Walk_Left"] = null;
        animatorOverrideController["Female_Walk_Right"] = null;
        animatorOverrideController["Female_Walk_Up"] = null;

        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }
}
