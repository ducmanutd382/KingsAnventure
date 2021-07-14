using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy3 : MonoBehaviour
{
    public float speed, circleRadius;
    private Rigidbody2D EnemyRB;
    public GameObject rightCheck, roofCheck, groundCheck;
    public LayerMask groundLayer;
    private bool facingRight = true, groundTouch, roofTouch, righttouch;
    public float dirX = 1, dirY = 0.25f;
    public Animator anim;

    public float startHP;
    [SerializeField] Image imgHP;

    public int dmg = 20;
    public float health = 40;
    [SerializeField] float timeToDestroy = 1;
    private bool vacham = false;
    private bool isDead = false;
    private Collider2D col;
    //[SerializeField] CircleCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        health = startHP;

        imgHP.fillAmount = 1;

        EnemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vacham == true && col.GetComponent<AttackRanger>().checkAtt == true)
        {
            Debug.Log("Ah");
            Damage(col.GetComponent<AttackRanger>().dmg);
            col.GetComponent<AttackRanger>().checkAtt = false;
            StartCoroutine(GetDamage());
        }



        EnemyRB.velocity = new Vector2(dirX, dirY) * speed * Time.deltaTime;
        HitDetection();

    }

    void HitDetection()
    {
        righttouch = Physics2D.OverlapCircle(rightCheck.transform.position, circleRadius, groundLayer);
        roofTouch = Physics2D.OverlapCircle(roofCheck.transform.position, circleRadius, groundLayer);
        groundTouch = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        HitLogic();
    }

    void HitLogic()
    {
        if (righttouch && facingRight)
        {
            Flip();
        }
        else if (righttouch && !facingRight)
        {
            Flip();
        }
        if (roofTouch)
        {
            dirY = -0.25f;
        }
        else if (groundTouch)
        {
            dirY = 0.25f;
        }


    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rightCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(roofCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
    }

    public void Damage(int damage)
    {

        health -= damage;

        imgHP.fillAmount = health / startHP;
    }

    private void Die()
    {
        if (health <= 0)
        {
            Debug.Log("chet cmn roi");
            anim.Play("Enemy2_Die");
            EnemyRB.velocity = Vector2.zero;
            //EnemyRB.isKinematic = true;
            //box.enabled = false;

            isDead = true;
            Destroy(gameObject, timeToDestroy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Attack"))
        {
            if (health > 0)
            {
                col = collider;
                vacham = true;
            }
            else
            {
                Die();
            }

        }
        if (collider.CompareTag("Bullet"))
        {
            if (health > 0)
            {
                anim.Play("Enemy2_Damaged");
                Damage(collider.gameObject.GetComponent<Bullet>().dmg);
            }
            else
            {
                Die();
            }

        }

    }

    IEnumerator GetDamage()
    {
        Debug.Log("qqq");
        anim.Play("Enemy2_Damaged");
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Enemy2_Idle");
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Attack"))
        {
            vacham = false;
            col = collider;
        }
    }

}
