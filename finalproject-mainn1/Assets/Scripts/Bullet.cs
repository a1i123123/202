using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Çarpýþma anýnda zombiye hasar verme iþlemi
            EnemyAiTutorial enemy = collision.gameObject.GetComponent<EnemyAiTutorial>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Mermiyi yok etme iþlemi
            Destroy(gameObject);
        }
    }
}
