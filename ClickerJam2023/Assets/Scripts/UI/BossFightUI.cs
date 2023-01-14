using UnityEngine;
using UnityEngine.UIElements;

public class BossFightUI : MonoBehaviour
{
    private VisualElement root;
    private VisualElement healthBar;
    private VisualElement fightButton;
    private Label healthValue;
    private Label fightStatusText;

    private void Awake()
    {
        InitializeDocument();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<VisualElement>("health");
        healthValue = root.Q<Label>("healthValue");
        fightStatusText = root.Q<Label>("fightStatusText");
        fightButton = root.Q<VisualElement>("fightButton");

        fightButton.RegisterCallback<MouseDownEvent>(StartBossFight);
    }

    public void UpdateBossHealth(float currentHealth, float maxHealth)
    {
        float healthPercent = currentHealth / maxHealth * 100f;
        healthBar.style.width = Length.Percent(healthPercent);
        healthValue.text = currentHealth + "/" + maxHealth;
    }

    public void UpdateFightStatusText(string text)
    {
        fightStatusText.text = text;
    }

    public void ActivateBossFightButton()
    {
        fightButton.SetEnabled(true);
        fightButton.AddToClassList("boss-button");
        fightButton.RemoveFromClassList("wave-button");
        fightStatusText.text = "Call the Boss!";
    }

    public void ActivateWaveButton()
    {
        fightButton.SetEnabled(false);
        fightButton.AddToClassList("wave-button");
        fightButton.RemoveFromClassList("boss-button");
    }

    public void DisableBossButton() => fightButton.SetEnabled(false);

    private void StartBossFight(MouseDownEvent evt) => FightManager.StartBossFight();
}
