using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float health = 1000;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] BoxCollider2D box;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] float velocity = 2f;
    [SerializeField] float jumpForce;
    [SerializeField] float timeToDestroy = 1f;

    public float startHP;
    [SerializeField] Image imgHP;


    [SerializeField] int currentSpeed = 0;
    [SerializeField] int director = 1;

    private bool isDead = false;


    //public GroundCheck ground;

    private int speedPr = Animator.StringToHash("Speed");
    private int jumpPr = Animator.StringToHash("Jump");
    private int isGroundPr = Animator.StringToHash("IsGround");
    private int dmgPr = Animator.StringToHash("Damaged");
    //public bool grounded = true;

    private void Start()
    {
        health = startHP;

        imgHP.fillAmount = 1;
    }

    private void Update()
    {
        
        if (!isDead)
        {
            InputHandle();
            UpdateAnimation();
            UpdateVelocity();
        }
        
    }

    private void UpdateVelocity()
    {
        //rotation by director
        transform.right = Vector2.right * director;

        var vx = currentSpeed * velocity * director;
        var vy = rigid.velocity.y;
        rigid.velocity = Vector2.right * vx
                        + Vector2.up * vy;
    }

    private void InputHandle()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSpeed = 1;
            director = -1;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            currentSpeed = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentSpeed = 1;
            director = 1;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            currentSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (groundCheck.isGround)
            {
                groundCheck.isGround = false;
                Jump();
            }
            
        }
    }

    public void Ins_LeftBtnDown_Clicked()
    {
        currentSpeed = 1;
        director = -1;
    }
    public void Ins_LeftBtnUp_Clicked()
    {
        currentSpeed = 0;
    }
    public void Ins_RightBtnDown_Clicked()
    {
        currentSpeed = 1;
        director = 1;
    }
    public void Ins_RightBtnUp_Clicked()
    {
        currentSpeed = 0;
    }
    public void Ins_JumpBtnDown_Clicked()
    {
        if (groundCheck.isGround)
        {
            groundCheck.isGround = false;
            Jump();
        }
    }


    private void UpdateAnimation()
    {
        anim.SetFloat(speedPr, currentSpeed);
        anim.SetBool(isGroundPr, groundCheck.isGround);
    }

    public void Jump()
    {
        
        rigid.AddForce(Vector2.up * jumpForce);
        anim.SetTrigger(jumpPr);
    }

    public void Damaged(int damage)
    {
        health -= damage;
        anim.SetTrigger(dmgPr);
        rigid.AddForce(Vector2.up * jumpForce);
        imgHP.fillAmount = health / startHP;
    }
    public void Trap_Damaged(int damage)
    {
        health -= damage;
        anim.SetTrigger(dmgPr);
        rigid.AddForce(Vector2.up * jumpForce);
        imgHP.fillAmount = health / startHP;
    }

    public void Bullet_Damaged(int damage)
    {
        health -= damage;
        anim.SetTrigger(dmgPr);
        imgHP.fillAmount = health / startHP;
    }

    private void Die()
    {
        if (health <= 0)
        {
            Debug.Log("chet cmn roi");
            anim.Play("Die");
            rigid.velocity = Vector2.zero;
            rigid.isKinematic = true;
            box.enabled = false;

            isDead = true;
            Destroy(gameObject, timeToDestroy);
            GameStateManager.CurrentState = GameState.Lose;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(health > 0)
            {
                if (collision.gameObject.GetComponent<EnemyController>())
                {
                    Damaged(collision.gameObject.GetComponent<EnemyController>().dmg);
                }
                else if (collision.gameObject.GetComponent<Enemy2>())
                {
                    Damaged(collision.gameObject.GetComponent<Enemy2>().dmg);
                }
                else if (collision.gameObject.GetComponent<TraManager>())
                {
                    Trap_Damaged(collision.gameObject.GetComponent<TraManager>().dmg);
                }
            
            }
            else
            {
                Die();
            }
            
        }
        else if (collision.gameObject.CompareTag("DoorToWin"))
        {
            GameStateManager.CurrentState = GameState.Win;
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            if(health > 0)
            {
                Debug.Log("khrbwehbfhkewbfckwevfe");
                GameStateManager.CurrentState = GameState.Lose;
            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy_Bullet"))
        {
            if (health > 0)
            {
                Bullet_Damaged(collision.gameObject.GetComponent<Enemy_Bullet>().dmg);
            }
            else
            {
                Die();
            }
        }
        
    }

    

}
