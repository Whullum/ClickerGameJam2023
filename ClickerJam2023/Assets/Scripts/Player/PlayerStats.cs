using UnityEngine;

public class PlayerStats
{
    public static int TotalGold { get; set; } = 0;
    public static int TotalGoldSpend { get; set; } = 0;
    public static int TotalPassiveGold { get; set; } = 0;
    public static int TotalDamageDealed { get; set; } = 0;
    public static int TotalEnemiesKilled { get; set; } = 0;
    public static int TotalUpgradesBought { get; set; } = 0;
    public static int TotalClickNumber { get; set; } = 0;
    public static float FastestBossKill { get; set; } = Mathf.Infinity;

    public static void LoadStats(StatsData data)
    {
        TotalGold = data.TotalGold;
        TotalGoldSpend = data.TotalGoldSpend;
        TotalPassiveGold = data.TotalPassiveGold;
        TotalDamageDealed = data.TotalDamageDealed;
        TotalEnemiesKilled = data.TotalEnemiesKilled;
        TotalUpgradesBought = data.TotalUpgradesBought;
        TotalClickNumber = data.TotalClickNumber;
        FastestBossKill = data.FastestBossKill;
    }
}
