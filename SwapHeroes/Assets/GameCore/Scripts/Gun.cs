using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPerfab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }
    }

    public void Ins_ShootBtnDown_Clicked()
    {
        Shoot();
    }

    void Shoot()
    {
        Instantiate(bulletPerfab, firePoint.position, firePoint.rotation);
    }
}
