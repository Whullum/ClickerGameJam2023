[System.Serializable]
public class GameData
{
    public UpgradeData[] Upgrades;
    public AreaData[] Areas;
    public WalletData Wallet;
    public StatsData Stats;

    public GameData(UpgradeData[] upgrades, AreaData[] areas, WalletData wallet, StatsData stats)
    {
        Upgrades = upgrades;
        Areas = areas;
        Wallet = wallet;
        Stats = stats;
    }
}
