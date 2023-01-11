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

    private void Awake()
    {
        InitializeBoss();
    }

    public bool DamageBoss(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
            return true;
        return false;
    }

    /// <summary>
    /// Used for when the boss is not defeated but another area is loaded.
    /// </summary>
    public void Despawn()
    {
        Destroy(gameObject);
    }

    private void InitializeBoss()
    {
        currentHealth = bossScriptable.MaxHealth;
    }
}
