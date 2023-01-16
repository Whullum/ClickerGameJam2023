using UnityEngine;
using UnityEngine.UIElements;

public class HubUI : MonoBehaviour
{
    private VisualElement root;
    private VisualElement hub;
    private VisualElement upgradesTab;
    private VisualElement resourcesTab;
    private VisualElement navigationTab;
    private VisualElement eventsTab;
    private VisualElement settingsTab;
    private VisualElement activeTab;
    private VisualElement upgradesPanel;
    private VisualElement resourcesPanel;
    private VisualElement navigationPanel;
    private VisualElement eventsPanel;
    private VisualElement settingsPanel;
    private VisualElement currentActivePanel;
    private Button toggleUIButton;

    private void Awake()
    {
        InitializeDocument();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        hub = root.Q<VisualElement>("hub");
        upgradesTab = root.Q<VisualElement>("upgrades");
        resourcesTab = root.Q<VisualElement>("resources");
        navigationTab = root.Q<VisualElement>("navigation");
        eventsTab = root.Q<VisualElement>("events");
        settingsTab = root.Q<VisualElement>("settings");
        upgradesPanel = root.Q<VisualElement>("UpgradesTab");
        resourcesPanel = root.Q<VisualElement>("ResourcesTab");
        navigationPanel = root.Q<VisualElement>("NavigationTab");
        eventsPanel = root.Q<VisualElement>("EventsTab");
        settingsPanel = root.Q<VisualElement>("SettingsTab");
        toggleUIButton = root.Q<Button>("toggleUIButton");

        upgradesTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        resourcesTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        navigationTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        eventsTab.RegisterCallback<MouseDownEvent>(ActivateTab);
        settingsTab.RegisterCallback<MouseDownEvent>(ActivateTab);

        toggleUIButton.clicked += ToggleUI;

        upgradesTab.RegisterCallback<MouseOverEvent>((type) =>
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Hover");
        });
        resourcesTab.RegisterCallback<MouseOverEvent>((type) =>
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Hover");
        });
        navigationTab.RegisterCallback<MouseOverEvent>((type) =>
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Hover");
        });
        eventsTab.RegisterCallback<MouseOverEvent>((type) =>
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Hover");
        });
        settingsTab.RegisterCallback<MouseOverEvent>((type) =>
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Hover");
        });

        upgradesPanel.style.display = DisplayStyle.None;
        resourcesPanel.style.display = DisplayStyle.None;
        navigationPanel.style.display = DisplayStyle.None;
        eventsPanel.style.display = DisplayStyle.None;
        settingsPanel.style.display = DisplayStyle.None;

        ActivateFirstTab();
    }

    private void ActivateTab(MouseDownEvent evt)
    {
        if (activeTab != null)
        {
            activeTab.RemoveFromClassList("tab-active");
            activeTab.AddToClassList("tab");
        }

        VisualElement tab = evt.currentTarget as VisualElement;

        tab.RemoveFromClassList("tab");
        tab.AddToClassList("tab-active");

        activeTab = tab;
        currentActivePanel.style.display = DisplayStyle.None;

        switch (activeTab.name)
        {
            case "upgrades":
                upgradesPanel.style.display = DisplayStyle.Flex;
                currentActivePanel = upgradesPanel;
                break;
            case "resources":
                resourcesPanel.style.display = DisplayStyle.Flex;
                currentActivePanel = resourcesPanel;
                break;
            case "navigation":
                navigationPanel.style.display = DisplayStyle.Flex;
                currentActivePanel = navigationPanel;
                break;
            case "events":
                eventsPanel.style.display = DisplayStyle.Flex;
                currentActivePanel = eventsPanel;
                break;
            case "settings":
                settingsPanel.style.display = DisplayStyle.Flex;
                currentActivePanel = settingsPanel;
                break;
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Click");
    }

    private void ActivateFirstTab()
    {
        upgradesPanel.style.display = DisplayStyle.Flex;
        upgradesTab.RemoveFromClassList("tab");
        upgradesTab.AddToClassList("tab-active");

        activeTab = upgradesTab;
        currentActivePanel = upgradesPanel;
    }

    private void ToggleUI()
    {
        hub.ToggleInClassList("hub-in");
        hub.ToggleInClassList("hub-out");
    }
}
