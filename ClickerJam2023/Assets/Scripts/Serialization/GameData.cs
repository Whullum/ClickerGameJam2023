[System.Serializable]
public class GameData
{
    public UpgradeData[] Upgrades;
    public AreaData[] Areas;
    public WalletData Wallet;

    public GameData(UpgradeData[] upgrades, AreaData[] areas, WalletData wallet)
    {
        Upgrades = upgrades;
        Areas = areas;
        Wallet = wallet;
    }
}
