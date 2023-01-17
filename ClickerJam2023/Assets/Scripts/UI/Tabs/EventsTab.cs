using UnityEngine.UIElements;
using UnityEngine;
using System.Collections;

public class EventsTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;
    private Label bossTitle;
    private Label bossLore;
    private Label areaTitle;
    private Label areaLore;
    private Label totalGold;
    private Label totalGoldSpend;
    private Label totalDamageDealed;
    private Label totalEnemiesKilled;
    private Label totalUpgradesBought;
    private Label totalClicks;
    private Label totalPassiveGold;
    private Label fastestBossKill;
    private float statsUpdateTime = 5.0f;

    private void Awake()
    {
        InitializeDocument();
    }

    private void Start()
    {
        if (AreaNavigation.AllAreas[0] != null)
            UpdateUI(AreaNavigation.AllAreas[0]);

        StartCoroutine(UpdatePlayerStats());
    }

    private void OnEnable()
    {
        AreaNavigation.AreaLoaded += UpdateUI;
    }

    private void OnDisable()
    {
        AreaNavigation.AreaLoaded -= UpdateUI;
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("EventsTab");
        content = root.Q<VisualElement>("container");
        bossTitle = content.Q<Label>("bossTitle");
        bossLore = content.Q<Label>("bossLoreText");
        areaTitle = content.Q<Label>("areaTitle");
        areaLore = content.Q<Label>("areaLoreText");
        totalGold = content.Q<Label>("totalGold");
        totalGoldSpend = content.Q<Label>("totalGoldSpend");
        totalPassiveGold = content.Q<Label>("totalPassiveGold");
        totalDamageDealed = content.Q<Label>("totalDamageDealed");
        totalEnemiesKilled = content.Q<Label>("totalEnemiesKilled");
        totalUpgradesBought = content.Q<Label>("totalUpgradesBought");
        totalClicks = content.Q<Label>("totalClicks");
        fastestBossKill = content.Q<Label>("fastestBossKill");
    }

    private void UpdateUI(Area areaData)
    {
        bossTitle.text = areaData.BossPrefab.BossData.Name;
        bossLore.text = areaData.BossPrefab.BossData.Lore;
        areaTitle.text = areaData.Name;
        areaLore.text = areaData.Lore;
    }

    private IEnumerator UpdatePlayerStats()
    {
        totalGold.text = PlayerStats.TotalGold.ToString();
        totalGoldSpend.text = PlayerStats.TotalGoldSpend.ToString();
        totalPassiveGold.text = PlayerStats.TotalPassiveGold.ToString();
        totalDamageDealed.text = PlayerStats.TotalDamageDealed.ToString();
        totalEnemiesKilled.text = PlayerStats.TotalEnemiesKilled.ToString();
        totalUpgradesBought.text = PlayerStats.TotalUpgradesBought.ToString();
        totalClicks.text = PlayerStats.TotalClickNumber.ToString();
        fastestBossKill.text = PlayerStats.FastestBossKill.ToString();

        yield return new WaitForSeconds(statsUpdateTime);

        StartCoroutine(UpdatePlayerStats());
    }
}
