using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg = 10;
    public float speed = 20f;
    public Rigidbody2D rigid;


    // Start is called before the first frame update
    void Start()
    {
        rigid.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShootRange") || collision.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
        }

    }

}
