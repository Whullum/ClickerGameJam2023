using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsNewGame
    {
        get { return isNewGame; }
    }

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
        if (!PlayerPrefs.HasKey(SerializationSystem.PlayerGameDataSave))
            isNewGame = true;
        else isNewGame = false;
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

    public void NewGame()
    {
        SerializationSystem.DeletePlayerData();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
