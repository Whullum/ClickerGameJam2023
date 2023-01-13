[System.Serializable]
public class GameData
{
    public UpgradeData[] Upgrades;
    public WalletData Wallet;

    public GameData(UpgradeData[] upgrades, WalletData wallet)
    {
        Upgrades = upgrades;
        Wallet = wallet;
    }
}
