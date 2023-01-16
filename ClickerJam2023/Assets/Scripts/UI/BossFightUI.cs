using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BossFightUI : MonoBehaviour
{
    private VisualElement root;
    private VisualElement healthBar;
    private VisualElement fightButton;
    private VisualElement fightFeedback;
    private Label healthValue;
    private Label fightStatusText;
    private Label fightName;
    private Label bossTime;
    private Label feedbackText;

    private void Awake()
    {
        InitializeDocument();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<VisualElement>("health");
        fightFeedback = root.Q<VisualElement>("fightFeedback");
        healthValue = root.Q<Label>("healthValue");
        fightStatusText = root.Q<Label>("fightStatusText");
        fightButton = root.Q<VisualElement>("fightButton");
        fightName = root.Q<Label>("fightName");
        bossTime = root.Q<Label>("bossTime");
        feedbackText = root.Q<Label>("feedbackText");

        fightButton.RegisterCallback<MouseDownEvent>(StartBossFight);
    }

    public void UpdateBossHealth(float currentHealth, float maxHealth)
    {
        if (currentHealth < 0) currentHealth = 0;

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
        bossTime.style.display = DisplayStyle.Flex;
    }

    public void ActivateWaveButton()
    {
        fightButton.SetEnabled(false);
        fightButton.AddToClassList("wave-button");
        fightButton.RemoveFromClassList("boss-button");
        bossTime.style.display = DisplayStyle.None;
    }

    public void DisableBossButton() => fightButton.SetEnabled(false);

    public void UpdateFightTitle(string title) => fightName.text = title;

    public void UpdateBossTimer(float time)
    {
        string timer = time.ToString("00.0");

        bossTime.text = timer;
    }

    public void ShowFeedback(string text, float time)
    {
        StartCoroutine(Feedback(text, time));
    }

    private IEnumerator Feedback(string text, float time)
    {
        feedbackText.text = text;
        fightFeedback.AddToClassList("feedback-in");
        fightFeedback.RemoveFromClassList("feedback-out");

        yield return new WaitForSeconds(time);

        fightFeedback.RemoveFromClassList("feedback-in");
        fightFeedback.AddToClassList("feedback-out");
    }

    private void StartBossFight(MouseDownEvent evt) => FightManager.StartBossFight();
}
