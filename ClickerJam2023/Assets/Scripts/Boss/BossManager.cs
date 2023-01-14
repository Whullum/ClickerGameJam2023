using System;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossEnemy ActiveBoss
    {
        get { return activeBoss; }
    }

    // When the current boss is defeated, this event is invoked.
    public static Action BossDefeated;

    // The current boss that the player is fighting.
    private static BossEnemy activeBoss;
    // Boss UI containing the boss health bar.
    private BossFightUI bossUI;
    private float damageTimer;

    [Tooltip("Point where the boss is going to be placed.")]
    [SerializeField] private Transform bossSpawnPoint;

    private void Awake()
    {
        bossUI = FindObjectOfType<BossFightUI>();
    }

    private void OnEnable()
    {
        AreaNavigation.AreaLoaded += SpawnBoss;
    }

    private void OnDisable()
    {
        AreaNavigation.AreaLoaded -= SpawnBoss;
    }

    private void Update()
    {
        IDLEBossHit();
    }

    /// <summary>
    /// If the boss is defeated, we destroy it and launch an event to update other systems.
    /// </summary>
    private void BossDeath()
    {
        Destroy(activeBoss.gameObject);

        BossDefeated?.Invoke();
    }

    /// <summary>
    /// Hits the boss with an amount of damage. Called from Unity UI Button.
    /// </summary>
    /// <param name="hitType">0 is active hit (player must click to do damage). 1 is idle hit (each second boss recives damage).</param>
    public void HitBoss(int hitType)
    {
        if (activeBoss == null) return; // Prevent this method to execute if no boss is currently active.

        // If the boss health is 0, we killed the boss.
        if (hitType == 0)
        {
            PlayerRevolver.IsClicking = true;

            if (activeBoss.DamageBoss(PlayerRevolver.Damage))
                BossDeath();
        }
        else if (hitType == 1)
        {
            if (activeBoss.DamageBoss(PlayerRevolver.IDLEDamage))
                BossDeath();
        }

        // Update the boss UI with the new health values.
        bossUI.UpdateBossHealth(activeBoss.CurrentHealth, activeBoss.BossData.MaxHealth);
    }

    /// <summary>
    /// Spawns the boss of the current active area.
    /// </summary>
    /// <param name="boss">The boss prefab to spawn.</param>
    private void SpawnBoss(Area areaData)
    {
        // If a boss is currently active, we despawn it.
        if (activeBoss != null)
            activeBoss.Despawn();

        // New boss is created and assigned to be the active boss.
        activeBoss = Instantiate(areaData.BossPrefab, bossSpawnPoint.position, Quaternion.identity);
        // Update the boss UI to match spawned boss params.
        bossUI.UpdateBossHealth(activeBoss.BossData.MaxHealth, activeBoss.BossData.MaxHealth);
    }

    private void IDLEBossHit()
    {
        if (damageTimer <= 0)
        {
            HitBoss(1);
            damageTimer = 1;
        }
        damageTimer -= Time.deltaTime;
    }
}
