using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackdelay = 0.3f;
    [SerializeField] bool attacking = false;

    [SerializeField] Animator anim;

    [SerializeField] Collider2D trigger;
    
    
    private int AttackPr = Animator.StringToHash("Attack");
    private void Awake()
    {
        //anim = gameObject.GetComponent<Animator>();
        //trigger.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !attacking)
        {
            attacking = true;
            //trigger.enabled = true;
            this.gameObject.transform.GetChild(2).GetComponent<AttackRanger>().checkAtt = true;
            attackdelay = 0.3f;
        }
        if (attacking)
        {
            if (attackdelay > 0)
            {
                attackdelay -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                //trigger.enabled = false;
                this.gameObject.transform.GetChild(2).GetComponent<AttackRanger>().checkAtt = false;
            }


        }
        

        anim.SetBool(AttackPr, attacking);
    }

    public void Ins_AttackBtnDown_Clicked()
    {
        attacking = true;
        //trigger.enabled = true;
        this.gameObject.transform.GetChild(2).GetComponent<AttackRanger>().checkAtt = true;
        attackdelay = 0.3f;
    }
}
