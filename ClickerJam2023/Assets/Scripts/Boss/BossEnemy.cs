using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public Boss BossData
    {
        get { return bossScriptable; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private float currentHealth;

    [SerializeField] private Boss bossScriptable;

    private void Start()
    {
        InitializeBoss();
    }

    public void DamageBoss(float damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
            Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void InitializeBoss()
    {
        currentHealth = bossScriptable.MaxHealth;
    }
}
