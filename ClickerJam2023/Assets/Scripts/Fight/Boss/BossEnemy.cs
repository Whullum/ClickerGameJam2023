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
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public float AliveTimeLeft
    {
        get { return aliveTime; }
    }

    private int currentHealth;
    private float aliveTime;
    private float currentBossKillTime;

    [Tooltip("The scriptable object of this boss.")]
    [SerializeField] private Boss bossScriptable;

    private void Awake()
    {
        InitializeBoss();
    }

    private void Start()
    {
        aliveTime = bossScriptable.ActiveTime;
    }

    private void Update()
    {
        DespawnTimer();
    }

    /// <summary>
    /// Damage the boss until it reaches 0.
    /// </summary>
    /// <param name="damageAmount">Amount of damage to recieve.</param>
    /// <returns>True if the boss is killed (health <= 0). False if not.</returns>
    public bool DamageBoss(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            AddBountyReward();

            if (currentBossKillTime < PlayerStats.FastestBossKill)
                PlayerStats.FastestBossKill = currentBossKillTime;

            return true;
        }

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
        currentBossKillTime = 0;
    }

    private void AddBountyReward()
    {
        // We give the player the reward
        PlayerWallet.AddMoney(bossScriptable.BountyReward);

        // Cool effects and sounds
    }

    private void DespawnTimer()
    {
        if (aliveTime <= 0)
        {
            Despawn();
            FightManager.StartWave();
        }
        aliveTime -= Time.deltaTime;
        currentBossKillTime += Time.deltaTime;            
    }

    public void RegenBoss()
    {
        if (currentHealth + bossScriptable.HealthRegenRatio > bossScriptable.MaxHealth)
            currentHealth = bossScriptable.MaxHealth;
        else
            currentHealth += bossScriptable.HealthRegenRatio;
    }
}
