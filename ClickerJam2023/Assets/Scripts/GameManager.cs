using UnityEngine;
using System.IO;
using System.Collections;

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

    private void Start()
    {
        StartCoroutine(AutoSaveData());
    }

    private void FirstSetup()
    {
        // We check if a base data exists, if not we create a new one.
        if (!PlayerPrefs.HasKey(SerializationSystem.BaseGameDataSave))
            SerializationSystem.SaveInitialUpgradeData();
    }

    private void LoadGameData()
    {
        GameData gameData = SerializationSystem.LoadGameData();

        UpgradeManager.LoadedUpgradeData = gameData.Upgrades;
        AreaNavigation.AreaData = gameData.Areas;
        PlayerWallet.LoadWalletData(gameData.Wallet.Wallet);
    }

    private IEnumerator AutoSaveData()
    {
        yield return new WaitForSeconds(10);
        SerializationSystem.SaveGameData(SerializationSystem.PlayerGameDataSave);
        StartCoroutine(AutoSaveData());
    }
}
