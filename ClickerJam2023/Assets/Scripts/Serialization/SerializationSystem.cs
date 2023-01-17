using UnityEngine;

public class SerializationSystem
{
    /// <summary>
    /// Path to the base upgrade values data.
    /// </summary>
    public static string BaseGameDataSave { get { return baseGameDataSave; } }
    public static string PlayerGameDataSave { get { return playerGameDataSave; } }

    private static string baseGameDataSave = "BaseData";
    private static string playerGameDataSave = "PlayerData";

    public static void SaveInitialUpgradeData()
    {
        SaveGameData(baseGameDataSave);

        Debug.Log("Success creating new game base data.");
    }

    public static GameData LoadGameData()
    {
        string loadID = baseGameDataSave;

        if (!GameManager.Instance.IsNewGame)
            loadID = playerGameDataSave;

        string gameData = PlayerPrefs.GetString(loadID);
        GameData data = JsonUtility.FromJson<GameData>(gameData);

        return data;
    }

    public static void SaveGameData(string id)
    {
        UpgradeData[] allUpgrades = SaveUpgradesData();
        AreaData[] areaData = SaveAreaData();
        WalletData wallet = SaveWalletData();
        StatsData stats = SaveStatsData();
        GameData gameData = new GameData(allUpgrades,areaData, wallet, stats);

        string savedData = JsonUtility.ToJson(gameData);

        PlayerPrefs.SetString(id, savedData);
        PlayerPrefs.Save();

        Debug.Log("Successfully saved game data.");
    }

    private static UpgradeData[] SaveUpgradesData()
    {
        var upgrades = Resources.LoadAll<Upgrade>("Upgrades");
        UpgradeData[] upgradesData = new UpgradeData[upgrades.Length];

        for (int i = 0; i < upgrades.Length; i++)
        {
            UpgradeData upgradeData = new UpgradeData(upgrades[i].GUID, upgrades[i].Cost, upgrades[i].Value, upgrades[i].TimesUnlocked, upgrades[i].Unlocked);
            upgradesData[i] = upgradeData;
        }

        return upgradesData;
    }

    public static void DeletePlayerData()
    {
        PlayerPrefs.DeleteKey(playerGameDataSave);
    }

    private static WalletData SaveWalletData()
    {
        return new WalletData(PlayerWallet.Wallet);
    }

    private static AreaData[] SaveAreaData()
    {
        var areas = Resources.LoadAll<Area>("Navigation");
        AreaData[] areaData = new AreaData[areas.Length];

        for(int i = 0; i < areaData.Length; i++)
        {
            AreaData savedData = new AreaData(areas[i].ID, areas[i].Unlocked, areas[i].Active);
            areaData[i] = savedData;
        }
        return areaData;
    }

    private static StatsData SaveStatsData()
    {
        return new StatsData(PlayerStats.TotalGold, PlayerStats.TotalGoldSpend, PlayerStats.TotalPassiveGold, PlayerStats.TotalDamageDealed,
            PlayerStats.TotalEnemiesKilled, PlayerStats.TotalUpgradesBought, PlayerStats.TotalClickNumber, PlayerStats.FastestBossKill);
    }
}
