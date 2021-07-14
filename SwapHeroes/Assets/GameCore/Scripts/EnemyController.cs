using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform partrol;

    public int dmg = 10;
    [SerializeField] float Health = 100;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] BoxCollider2D box;
    [SerializeField] float jumpForce;
    [SerializeField] float timeToDestroy = 1;

    [SerializeField] float velocity = 2;
    [SerializeField] int currentSpeed = 0;
    [SerializeField] int director = 1;

    public float startHP;
    [SerializeField] Image imgHP;

    private Collider2D col;

    private Vector3 startPos, endPos;
    private int currentPointIndex = 0;
    private List<Vector2> positions = new List<Vector2>();

    //private int dmgPr = Animator.StringToHash("Enemy_Damaged");
    //private int speedPr = Animator.StringToHash("Enemy_Run");
    private bool vacham = false;
    //private int diePr = Animator.StringToHash("Enemy_Die");
    private bool isDead = false;

    private void Start()
    {
        Health = startHP;

        imgHP.fillAmount = 1;

        if (partrol != null)
        {
            for (int i = 0; i < partrol.childCount; i++)
                positions.Add(partrol.GetChild(i).position);
        }

        if (positions.Count > 0)
        {
            currentPointIndex = -1;
            GoToNextPoint();
        }
    }




    // Update is called once per frame
    void Update()
    {
        //Die();
        //UpdateVelocity();
        UpdateAnimation();
        if (vacham == true && col.GetComponent<AttackRanger>().checkAtt == true)
        {
            Debug.Log("Ah");
            Damage(col.GetComponent<AttackRanger>().dmg);
            col.GetComponent<AttackRanger>().checkAtt = false;
            StartCoroutine(GetDamage());
        }

        if (isDead)
            return;
        if (positions.Count <= 0)
            return;

        var pos = transform.position;
        var offset = endPos - pos;

        if (offset.magnitude > 0.1)
        {
            rigid.velocity = offset.normalized * velocity;
        }
        else
        {
            rigid.velocity = Vector2.zero;
            GoToNextPoint();
        }
        //anim.SetFloat(speedPr, currentSpeed);
    }

    private void GoToNextPoint()
    {
        currentPointIndex++;
        currentPointIndex = currentPointIndex % positions.Count;
        var nextPointIndex = (currentPointIndex + 1) % positions.Count;

        startPos = positions[currentPointIndex];
        endPos = positions[nextPointIndex];
        transform.position = startPos;
    }

    private void Die()
    {
        if (Health <= 0)
        {
            Debug.Log("chet cmn roi");
            anim.Play("Enemy_Die");
            rigid.velocity = Vector2.zero;
            rigid.isKinematic = true;
            box.enabled = false;

            isDead = true;
            Destroy(gameObject, timeToDestroy);
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

    private void UpdateAnimation()
    {
        //anim.SetFloat(speedPr, currentSpeed);
        
        //anim.SetBool(isGroundPr, groundCheck.isGround);
    }
    public void Damage(int damage)
    {
        //anim.SetTrigger(dmgPr);
        //rigid.AddForce(Vector2.up * jumpForce);
        Health -= damage;

        imgHP.fillAmount = Health / startHP;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Attack"))
        {
            if(Health > 0)
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
            if(Health > 0)
            {
                anim.Play("Enemy_Damaged");
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
        anim.Play("Enemy_Damaged");
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Enemy_Run");
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
