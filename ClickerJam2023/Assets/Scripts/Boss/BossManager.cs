using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Singleton<BossManager>
{
    public BossEnemy ActiveBoss
    {
        get { return activeBoss; }
    }

    // The current boss that the player is fighting.
    private BossEnemy activeBoss;
    private BossFightUI bossUI;

    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private BossEnemy[] allBosses;
    [SerializeField] private int bossIDToSpawn = 0; // Just for testing purposes.

    protected override void Awake()
    {
        base.Awake();

        bossUI = FindObjectOfType<BossFightUI>();
    }

    private void Start()
    {
        SpawnBoss(bossIDToSpawn);
    }

    public void HitBoss()
    {
        float damage = 1; // Here we need to use the player damage.

        activeBoss.DamageBoss(damage);
        bossUI.UpdateBossHealth(activeBoss.CurrentHealth, activeBoss.BossData.MaxHealth);
    }

    private void SpawnBoss(int ID)
    {
        for(int i = 0; i < allBosses.Length; i++)
        {
            if (allBosses[i].BossData.ID == ID)
            {
                activeBoss = Instantiate(allBosses[i], bossSpawnPoint.position, Quaternion.identity);
                return;
            }
        }
    }
}
