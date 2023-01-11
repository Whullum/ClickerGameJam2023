using System;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public BossEnemy ActiveBoss
    {
        get { return activeBoss; }
    }
    public static Action BossDefeated;

    // The current boss that the player is fighting.
    private BossEnemy activeBoss;
    private BossFightUI bossUI;

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
    public void HitBoss()
    {
        if (activeBoss == null) return; // If no boss is currently in scene, we cannot continue.

        if (activeBoss.DamageBoss(PlayerClick.Damage))
            BossDeath();

        bossUI.UpdateBossHealth(activeBoss.CurrentHealth, activeBoss.BossData.MaxHealth);
    }

    /// <summary>
    /// Spawns the boss of the current active area.
    /// </summary>
    /// <param name="boss">The boss prefab to spawn.</param>
    private void SpawnBoss(BossEnemy boss)
    {
        if (activeBoss != null) // If a boss is currently active, we despawn it.
            activeBoss.Despawn();

        activeBoss = Instantiate(boss, bossSpawnPoint.position, Quaternion.identity); // We spawn the boss of the current area.
        bossUI.UpdateBossHealth(activeBoss.BossData.MaxHealth, activeBoss.BossData.MaxHealth); // Update the boss UI to match spawned boss params.
    }
}
