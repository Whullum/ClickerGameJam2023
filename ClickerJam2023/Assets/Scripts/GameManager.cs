using UnityEngine;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    public bool IsNewGame
    {
        get { return isNewGame; }
    }

    [SerializeField]
    private bool isNewGame;

    protected override void Awake()
    {
        base.Awake();

        FirstSetup();
        LoadGameData();
    }

    private void FirstSetup()
    {
        // We check if a base data exists, if not we create a new one.
        if (!File.Exists(SerializationSystem.BaseGameDataSave))
            SerializationSystem.SaveInitialUpgradeData();
    }

    private void LoadGameData()
    {
        GameData gameData = SerializationSystem.LoadGameData();

        UpgradeManager.LoadedUpgradeData = gameData.Upgrades;
        PlayerWallet.LoadWalletData(gameData.Wallet.Wallet);
    }

    private void OnApplicationQuit()
    {
        SerializationSystem.SaveGameData(SerializationSystem.PlayerGameDataSave);
    }
}
