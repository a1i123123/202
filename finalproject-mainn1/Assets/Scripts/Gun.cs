using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public void Fire()
    {
        // Mermi prefabinden kopya olu�tur
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Mermiyi hareket ettirme i�lemini BulletController scriptine b�rak�yoruz.
    }
}
