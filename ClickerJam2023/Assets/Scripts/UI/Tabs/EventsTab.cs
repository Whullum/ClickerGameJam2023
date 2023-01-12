using UnityEngine.UIElements;
using UnityEngine;
using TreeEditor;

public class EventsTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;
    private Label bossTitle;
    private Label bossLore;
    private Label areaTitle;
    private Label areaLore;

    private void Awake()
    {
        InitializeDocument();
    }

    private void Start()
    {
        if (AreaNavigation.AllAreas[0] != null)
            UpdateUI(AreaNavigation.AllAreas[0]);
    }

    private void OnEnable()
    {
        AreaNavigation.AreaLoaded += UpdateUI;
    }

    private void OnDisable()
    {
        AreaNavigation.AreaLoaded -= UpdateUI;
    }

    private void UpdateUI(Area areaData)
    {
        bossTitle.text = areaData.BossPrefab.BossData.Name;
        bossLore.text = areaData.BossPrefab.BossData.Lore;
        areaTitle.text = areaData.Name;
        areaLore.text = areaData.Lore;
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("EventsTab");
        content = root.Q<VisualElement>("container");
        bossTitle = content.Q<Label>("bossTitle");
        bossLore = content.Q<Label>("bossLoreText");
        areaTitle = content.Q<Label>("areaTitle");
        areaLore = content.Q<Label>("areaLoreText");
    }
}
