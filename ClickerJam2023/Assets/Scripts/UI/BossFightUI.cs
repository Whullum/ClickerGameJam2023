using UnityEngine;
using UnityEngine.UIElements;

public class BossFightUI : MonoBehaviour
{
    private VisualElement root;
    private VisualElement healthBar;

    private void Awake()
    {
        InitializeDocument();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<VisualElement>("health");   
    }

    public void UpdateBossHealth(float currentHealth, float maxHealth)
    {
        float healthPercent = currentHealth / maxHealth * 100f;
        healthBar.style.width = Length.Percent(healthPercent);
    }
}
