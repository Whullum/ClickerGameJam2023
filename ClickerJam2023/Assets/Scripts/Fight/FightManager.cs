using UnityEngine;

public class FightManager : MonoBehaviour
{
    private static FightState fightState;
    private static BossFightUI fightUI;
    private static BossManager bossManager;
    private static WaveSystem waveSystem;
    private float hitTimer;
    private static bool canUpdateUI = true; // For keeping the hitWave from updating the UI once all the wave is defeated.

    private void Awake()
    {
        fightUI = FindObjectOfType<BossFightUI>();
        bossManager = FindObjectOfType<BossManager>();
        waveSystem = FindObjectOfType<WaveSystem>();
    }

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        PassiveHit();
    }

    /// <summary>
    /// Called from Unity UI Button.
    /// </summary>
    public void RevolverHit()
    {
        if (fightState == FightState.Wave)
            HitWave(HitType.Active);
        if (fightState == FightState.Boss)
            HitBoss(HitType.Active);
    }

    private void HitBoss(HitType hitType)
    {
        bossManager.HitBoss(hitType);

        // Update the boss UI with the new health values.
        fightUI.UpdateBossHealth(BossManager.ActiveBoss.CurrentHealth, BossManager.ActiveBoss.BossData.MaxHealth);
    }

    private void HitWave(HitType hitType)
    {
        waveSystem.HitWave(hitType);

        if (canUpdateUI)
        {
            fightUI.UpdateBossHealth(waveSystem.CurrentEnemyHealth, waveSystem.CurrentMaxEnemyHealth);
            fightUI.UpdateFightStatusText("Enemies left " + waveSystem.EnemiesLeft.ToString());
        }
    }

    private void PassiveHit()
    {
        if (hitTimer <= 0)
        {
            if (fightState == FightState.Wave)
                HitWave(HitType.Passive);
            if (fightState == FightState.Boss)
                HitBoss(HitType.Passive);

            hitTimer = 1;
        }
        hitTimer -= Time.deltaTime;
    }

    public static void EnableBossCallButton()
    {
        canUpdateUI = false;
        fightUI.ActivateBossFightButton();
    }

    public static void StartWave()
    {
        canUpdateUI = true;

        fightState = FightState.Wave;

        waveSystem.SpawnWave();

        fightUI.ActivateWaveButton();
    }

    public static void StartBossFight()
    {
        fightState = FightState.Boss;

        bossManager.StartBossFight();

        fightUI.DisableBossButton();
    }
}
