using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationSystem
{
    /// <summary>
    /// Path to the base upgrade values data.
    /// </summary>
    public static string BaseGameDataSave { get { return baseGameDataSave; } }
    public static string PlayerGameDataSave { get { return playerGameDataSave; } }

    private static string baseGameDataSave = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "baseData.dat";
    private static string playerGameDataSave = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "playerData.dat";

    public static void SaveInitialUpgradeData()
    {
        SaveGameData(baseGameDataSave);

        Debug.Log("Sucess creating new game base data.");
    }

    public static GameData LoadGameData()
    {
        string loadPath = baseGameDataSave;

        if (!GameManager.Instance.IsNewGame)
            loadPath = playerGameDataSave;

        if(!File.Exists(loadPath))
        {
            Debug.LogWarning("Cannot load save file. File not found.");
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(loadPath, FileMode.Open);
        GameData gameData = formatter.Deserialize(file) as GameData;

        file.Close();

        return gameData;
    }

    public static void SaveGameData(string path)
    {
        UpgradeData[] allUpgrades = SaveUpgradesData();
        WalletData wallet = SaveWalletData();
        GameData gameData = new GameData(allUpgrades, wallet);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(path);
        formatter.Serialize(file, gameData);

        file.Close();
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

    private static WalletData SaveWalletData()
    {
        return new WalletData(PlayerWallet.Wallet);
    }
}
