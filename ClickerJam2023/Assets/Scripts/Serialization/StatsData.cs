[System.Serializable]
public class StatsData
{
    public int TotalGold;
    public int TotalGoldSpend;
    public int TotalPassiveGold;
    public int TotalDamageDealed;
    public int TotalEnemiesKilled;
    public int TotalUpgradesBought;
    public int TotalClickNumber;
    public float FastestBossKill;

    public StatsData(int totalGold, int totalGoldSpend, int totalPassiveGold, int totalDamageDealed, int totalEnemiesKilled, int totalUpgradesBought, int totalClickNumber, float fastestBossKill)
    {
        TotalGold = totalGold;
        TotalGoldSpend = totalGoldSpend;
        TotalPassiveGold = totalPassiveGold;
        TotalDamageDealed = totalDamageDealed;
        TotalEnemiesKilled = totalEnemiesKilled;
        TotalUpgradesBought = totalUpgradesBought;
        TotalClickNumber = totalClickNumber;
        FastestBossKill = fastestBossKill;
    }
}
