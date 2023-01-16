using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int CurrentEnemyHealth { get { return currentEnemyHealth; } }
    public int CurrentMaxEnemyHealth { get { return currentMaxEnemyHealth; } }
    public int EnemiesLeft { get { return enemyCount; } }
    public string WaveName { get { return waveData.WaveName; } }

    private Wave waveData;
    private ParticleSystem enemyDeathEffect;
    private SpriteRenderer enemySprite;
    private int enemyCount;
    private int currentEnemyHealth;
    private int currentMaxEnemyHealth;
    private int currentEnemyReward;

    private void Awake()
    {
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        enemyDeathEffect = GameObject.Find("EnemyDeathEffect").GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        AreaNavigation.AreaLoaded += LoadWaveData;
    }

    private void OnDisable()
    {
        AreaNavigation.AreaLoaded -= LoadWaveData;
    }

    private void LoadWaveData(Area data)
    {
        waveData = data.Wave;

        SpawnWave();
    }

    public void SpawnWave()
    {
        enemyCount = waveData.EnemyCount;

        ParticleSystemRenderer particleMaterial = enemyDeathEffect.GetComponent<ParticleSystemRenderer>();
        particleMaterial.material.mainTexture = waveData.EnemySprite;
        enemySprite.gameObject.SetActive(true);
        enemySprite.sprite = Sprite.Create(waveData.EnemySprite, new Rect(0, 0, waveData.EnemySprite.width, waveData.EnemySprite.height), new Vector2(0.5f, 0.5f));
        CreateNewEnemy();
    }

    public void HitWave(HitType hitType)
    {
        int totalEnemies = enemyCount;
        int playerDamage = 0;
        int totalEnemiesKilled = 0; // Used for particle generation.

        if (hitType == HitType.Active)
            playerDamage = PlayerRevolver.Damage;
        if (hitType == HitType.Passive)
            playerDamage = PlayerRevolver.PassiveDamage;

        for (int i = 0; i < totalEnemies; i++)
        {
            if (playerDamage - currentEnemyHealth < 0)
            {
                currentEnemyHealth = currentEnemyHealth - playerDamage;
                break;
            }
            if (playerDamage - currentEnemyHealth >= 0)
            {
                playerDamage -= currentEnemyHealth;
                currentEnemyHealth = currentMaxEnemyHealth;
                enemyCount--;
                PlayerWallet.AddMoney(currentEnemyReward);
                totalEnemiesKilled++;
                CreateNewEnemy();

                if (enemyCount <= 0)
                {
                    enemySprite.gameObject.SetActive(false);
                    FightManager.EnableBossCallButton();
                }
            }
        }
        StartCoroutine(CreateDeathEnemyParticle(totalEnemiesKilled));
    }

    private void CreateNewEnemy()
    {
        currentMaxEnemyHealth = Random.Range(waveData.MinEnemyHealth, waveData.MaxEnemyHealth + 1);
        currentEnemyReward= Random.Range(waveData.MinGoldPerKill, waveData.MaxGoldPerKill + 1);
        currentEnemyHealth = currentMaxEnemyHealth;
    }

    private IEnumerator CreateDeathEnemyParticle(int deathCount)
    {
        for(int i = 0; i < deathCount; i++)
        {
            enemyDeathEffect.Play();
            yield return null;
        }
    }
}
