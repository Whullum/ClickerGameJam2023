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
    private VisualElement merchantDroidButton;
    private VisualElement cargoDroidButton;
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
    [SerializeField] private Upgrade merchantDroidUpgrade;
    [SerializeField] private Upgrade cargoDroidUpgrade;
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
        merchantDroidButton = content.Q<VisualElement>("merchantDroidButton");
        cargoDroidButton = content.Q<VisualElement>("cargoDroidButton");
        smartInvestmentsButton = content.Q<VisualElement>("smartInvestmentsButton");
        wallet = content.Q<Label>("wallet");

        /*reinforcedBarrelButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, reinforcedBarrelUpgrade);
        explodingRoundsButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, explodingRoundsUpgrade);
        uraniumLacedPointsButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, uraniumLacedPointsUpgrade);
        combatDroidButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, combatDroidUpgrade);
        rocketDroidButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, rocketDroidUpgrade);
        swarmerDroidButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, swarmerDroidUpgrade);
        megaDroidButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, megaDroidUpgrade);
        biggerPocketsButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, biggerPocketsUpgrade);
        eagleEyeButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, eagleEyeUpgrade);
        miningDroidButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, miningDroidUpgrade);
        merchantDroidButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, merchantDroidUpgrade);
        cargoDroidButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, cargoDroidUpgrade);
        smartInvestmentsButton.RegisterCallback<MouseDownEvent, Upgrade>(ClickButton, smartInvestmentsUpgrade);*/

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
        allButtons.Add(merchantDroidUpgrade.GUID, merchantDroidButton);
        allButtons.Add(cargoDroidUpgrade.GUID, cargoDroidButton);
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
        foreach (KeyValuePair<string, VisualElement> item in allButtons)
        {
            Upgrade data = UpgradeManager.GetUpgrade(item.Key);

            if (item.Key == data.GUID)
            {
                Label cost = item.Value.Q<Label>("cost");
                Label value = item.Value.Q<Label>("value");

                cost.text = data.Cost.ToString();
                value.text = data.Value.ToString();

                if (data.Cost > PlayerWallet.Wallet || !data.Unlocked)
                    item.Value.SetEnabled(false);
                else item.Value.SetEnabled(true);
            }
        }

        wallet.text = PlayerWallet.Wallet.ToString();
    }
}
