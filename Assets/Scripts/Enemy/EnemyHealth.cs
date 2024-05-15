using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    bool isDead = false;
    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        BroadcastMessage("OnDamageTaken");
        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("Die");
    }
}
