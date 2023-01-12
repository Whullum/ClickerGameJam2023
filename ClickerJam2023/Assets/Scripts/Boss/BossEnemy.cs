using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    /// <summary>
    /// ScriptableObject containing boss data such as ID, name, etc.
    /// </summary>
    public Boss BossData
    {
        get { return bossScriptable; }
    }

    /// <summary>
    /// Current health of this boss.
    /// </summary>
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private float currentHealth;

    [Tooltip("The scriptable object of this boss.")]
    [SerializeField] private Boss bossScriptable;

    private void Awake()
    {
        InitializeBoss();
    }

    /// <summary>
    /// Damage the boss until it reaches 0.
    /// </summary>
    /// <param name="damageAmount">Amount of damage to recieve.</param>
    /// <returns>True if the boss is killed (health <= 0). False if not.</returns>
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

    /// <summary>
    /// Initialization of the boss properties.
    /// </summary>
    private void InitializeBoss()
    {
        currentHealth = bossScriptable.MaxHealth;
    }
}