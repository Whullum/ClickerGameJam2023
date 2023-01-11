using UnityEngine;
using UnityEngine.UIElements;

public class HubUI : MonoBehaviour
{
    private VisualElement root;
    private VisualElement upgradesTab;
    private VisualElement resourcesTab;
    private VisualElement navigationTab;
    private VisualElement eventsTab;
    private VisualElement settingsTab;
    private VisualElement activeTab;

    private void Awake()
    {
        InitializeDocument();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        upgradesTab = root.Q<VisualElement>("upgrades");
        resourcesTab = root.Q<VisualElement>("resources");
        navigationTab = root.Q<VisualElement>("navigation");
        eventsTab = root.Q<VisualElement>("events");
        settingsTab = root.Q<VisualElement>("settings");

        upgradesTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        resourcesTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        navigationTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        eventsTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        settingsTab.RegisterCallback<MouseDownEvent>(ActivateTab);
    }

    private void ActivateTab(MouseDownEvent evt)
    {
        if(activeTab != null)
        {
            activeTab.RemoveFromClassList("tab-active");
            activeTab.AddToClassList("tab");
        }

        VisualElement tab = evt.currentTarget as VisualElement;

        tab.RemoveFromClassList("tab");
        tab.AddToClassList("tab-active");

        activeTab = tab;
    }
}
