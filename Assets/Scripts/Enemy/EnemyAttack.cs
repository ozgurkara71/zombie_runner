using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 20f;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    // We bound Animation event to below function
    public void AttackHitEvent()
    {
        if (!target) return;

        target.DecreaseHealth(damage);
        // Vfx that shows to player has taken damage
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }
}
