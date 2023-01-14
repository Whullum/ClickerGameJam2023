using System;
using TreeEditor;
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
    private Area areaData;
    private ParticleSystem revolverHitEffect;

    [Tooltip("Point where the boss is going to be placed.")]
    [SerializeField] private Transform bossSpawnPoint;

    private void Awake()
    {
        revolverHitEffect = GameObject.Find("RevolverHitEffect").GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        AreaNavigation.AreaLoaded += LoadAreaData;
    }

    private void OnDisable()
    {
        AreaNavigation.AreaLoaded -= LoadAreaData;
    }

    /// <summary>
    /// If the boss is defeated, we destroy it and launch an event to update other systems.
    /// </summary>
    private void BossDeath()
    {
        Destroy(activeBoss.gameObject);
        
        BossDefeated?.Invoke();

        FightManager.StartWave();
    }

    /// <summary>
    /// Hits the boss with player defined damage.
    /// </summary>
    public void HitBoss(HitType hitType)
    {
        if (activeBoss == null) return; // Prevent this method to execute if no boss is currently active.

        int playerDamage = 0;

        if (hitType == HitType.Active)
        {
            RevolverHitEffect();
            playerDamage = PlayerRevolver.Damage;
        }
        else if (hitType == HitType.Passive)
            playerDamage = PlayerRevolver.PassiveDamage;

        if (activeBoss.DamageBoss(playerDamage))
            BossDeath();
    }

    private void LoadAreaData(Area data) => areaData = data;

    private void RevolverHitEffect()
    {
        var hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hitPosition.z = 0;

        revolverHitEffect.transform.position = hitPosition;
        revolverHitEffect.Play();
    }

    public void StartBossFight()
    {
        // If a boss is currently active, we despawn it.
        if (activeBoss != null)
            activeBoss.Despawn();

        // New boss is created and assigned to be the active boss.
        activeBoss = Instantiate(areaData.BossPrefab, bossSpawnPoint.position, Quaternion.identity);
    }
}
