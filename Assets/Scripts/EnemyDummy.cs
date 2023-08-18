using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    public int HP = 5;

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.tag == "Weapon")
        {
            HP--;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
