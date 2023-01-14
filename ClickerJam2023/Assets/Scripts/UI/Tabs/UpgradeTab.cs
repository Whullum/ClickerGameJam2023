using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeTab : MonoBehaviour
{
    public static Action<string> UpgradeButtonClick;

    private VisualElement root;
    private VisualElement content;
    // Revolver buttons
    private VisualElement reinforcedBarrelButton;
    private VisualElement explodingRoundsButton;
    private VisualElement uraniumLacedPointsButton;
    // IDLE Revolver buttons
    private VisualElement combatDroidButton;
    private VisualElement rocketDroidButton;
    private VisualElement swarmerDroidButton;
    private VisualElement megaDroidButton;
    // Gold buttons
    private VisualElement biggerPocketsButton;
    private VisualElement eagleEyeButton;
    // IDLE gold buttons
    private VisualElement miningDroidButton;
    private VisualElement cargoDroidButton;
    private VisualElement merchantDroidButton;
    private VisualElement smartInvestmentsButton;
    private Label wallet;

    private Dictionary<string, VisualElement> allButtons = new Dictionary<string, VisualElement>();

    [SerializeField] private Upgrade reinforcedBarrelUpgrade;
    [SerializeField] private Upgrade explodingRoundsUpgrade;
    [SerializeField] private Upgrade uraniumLacedPointsUpgrade;
    [SerializeField] private Upgrade combatDroidUpgrade;
    [SerializeField] private Upgrade rocketDroidUpgrade;
    [SerializeField] private Upgrade swarmerDroidUpgrade;
    [SerializeField] private Upgrade megaDroidUpgrade;
    [SerializeField] private Upgrade biggerPocketsUpgrade;
    [SerializeField] private Upgrade eagleEyeUpgrade;
    [SerializeField] private Upgrade miningDroidUpgrade;
    [SerializeField] private Upgrade cargoDroidUpgrade;
    [SerializeField] private Upgrade merchantDroidUpgrade;
    [SerializeField] private Upgrade smartInvestmentsUpgrade;

    private void Awake()
    {
        InitializeDocument();
    }

    private void OnEnable()
    {
        PlayerWallet.WalletUpdated += UpdateButtons;
    }

    private void OnDisable()
    {
        PlayerWallet.WalletUpdated -= UpdateButtons;
    }

    private void Start()
    {
        UpdateButtons();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("UpgradesTab");
        content = root.Q<VisualElement>("container");
        reinforcedBarrelButton = content.Q<VisualElement>("reinforcedBarrelButton");
        explodingRoundsButton = content.Q<VisualElement>("explodingRoundsButton");
        uraniumLacedPointsButton = content.Q<VisualElement>("uraniumLacedPointsButton");
        combatDroidButton = content.Q<VisualElement>("combatDroidButton");
        rocketDroidButton = content.Q<VisualElement>("rocketDroidButton");
        swarmerDroidButton = content.Q<VisualElement>("swarmerDroidButton");
        megaDroidButton = content.Q<VisualElement>("megaDroidButton");
        biggerPocketsButton = content.Q<VisualElement>("biggerPocketsButton");
        eagleEyeButton = content.Q<VisualElement>("eagleEyeButton");
        miningDroidButton = content.Q<VisualElement>("miningDroidButton");
        cargoDroidButton = content.Q<VisualElement>("cargoDroidButton");
        merchantDroidButton = content.Q<VisualElement>("merchantDroidButton");
        smartInvestmentsButton = content.Q<VisualElement>("smartInvestmentsButton");
        wallet = content.Q<Label>("wallet");

        allButtons.Add(reinforcedBarrelUpgrade.GUID, reinforcedBarrelButton);
        allButtons.Add(explodingRoundsUpgrade.GUID, explodingRoundsButton);
        allButtons.Add(uraniumLacedPointsUpgrade.GUID, uraniumLacedPointsButton);
        allButtons.Add(combatDroidUpgrade.GUID, combatDroidButton);
        allButtons.Add(rocketDroidUpgrade.GUID, rocketDroidButton);
        allButtons.Add(swarmerDroidUpgrade.GUID, swarmerDroidButton);
        allButtons.Add(megaDroidUpgrade.GUID, megaDroidButton);
        allButtons.Add(biggerPocketsUpgrade.GUID, biggerPocketsButton);
        allButtons.Add(eagleEyeUpgrade.GUID, eagleEyeButton);
        allButtons.Add(miningDroidUpgrade.GUID, miningDroidButton);
        allButtons.Add(cargoDroidUpgrade.GUID, cargoDroidButton);
        allButtons.Add(merchantDroidUpgrade.GUID, merchantDroidButton);
        allButtons.Add(smartInvestmentsUpgrade.GUID, smartInvestmentsButton);

        foreach (KeyValuePair<string, VisualElement> item in allButtons)
        {
            item.Value.RegisterCallback<MouseDownEvent, string>(ClickButton, item.Key);
        }
    }

    private void ClickButton(MouseDownEvent evt, string upgradeGuid)
    {
        VisualElement button = evt.currentTarget as VisualElement;

        UpgradeButtonClick?.Invoke(upgradeGuid);

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        var nextUpgradeUnlockCount = 0;

        foreach (KeyValuePair<string, VisualElement> item in allButtons)
        {
            Upgrade data = UpgradeManager.GetUpgrade(item.Key);

            if (item.Key == data.GUID)
            {
                if (data.Unlocked)
                {
                    item.Value.RemoveFromClassList("button-locked");
                    item.Value.SetEnabled(true);

                    UQueryBuilder<VisualElement> builder = new UQueryBuilder<VisualElement>(item.Value);
                    List<VisualElement> result = builder.Build().ToList();

                    result.ForEach(element =>
                    {
                        element.style.display = DisplayStyle.Flex;
                    });

                    Label cost = item.Value.Q<Label>("cost");
                    Label value = item.Value.Q<Label>("value");
                    Label nextValue = item.Value.Q<Label>("nextValue");

                    cost.text = data.Cost.ToString();
                    value.text = data.Value.ToString();
                    nextValue.text = data.NextValue.ToString();

                    if (data.Cost > PlayerWallet.Wallet)
                        item.Value.SetEnabled(false);
                    else item.Value.SetEnabled(true);
                }
                else
                {
                    item.Value.AddToClassList("button-locked");
                    item.Value.SetEnabled(false);

                    UQueryBuilder<VisualElement> builder = new UQueryBuilder<VisualElement>(item.Value);
                    List<VisualElement> result = builder.Build().ToList();

                    result.ForEach(element =>
                    {
                        Label cost = item.Value.Q<Label>("cost");

                        if (!element.Equals(item.Value))
                            element.style.display = DisplayStyle.None;
                        if (element.ClassListContains("revolver-data-container"))
                            element.style.display = DisplayStyle.Flex;

                        cost.style.display = DisplayStyle.Flex;
                        cost.text = $"Upgrades until unlock\n\n" + nextUpgradeUnlockCount.ToString();
                    });
                }
            }
            nextUpgradeUnlockCount = data.NextUpgradeUnlockCount - data.TimesUnlocked;
        }

        wallet.text = PlayerWallet.Wallet.ToString();
    }
}
