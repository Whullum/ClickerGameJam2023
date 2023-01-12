using UnityEngine;
using UnityEngine.UIElements;

public class ResourcesTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;
    private Label wallet;
    private Label ratio;
    private Label income;
    private Label totalSpend;
    private Label companionLore;

    private void Awake()
    {
        InitializeDocument();
    }

    private void Start()
    {
        wallet.text = PlayerWallet.Wallet.ToString();
        income.text = PlayerWallet.MoneyIncome.ToString();
    }

    private void OnEnable()
    {
        PlayerWallet.WalletUpdated += UpdateUI;
        AreaNavigation.AreaLoaded += UpdateCompanionLore;
    }

    private void OnDisable()
    {
        PlayerWallet.WalletUpdated -= UpdateUI;
        AreaNavigation.AreaLoaded -= UpdateCompanionLore;
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("ResourcesTab");
        content = root.Q<VisualElement>("container");
        wallet = content.Q<Label>("wallet");
        ratio = content.Q<Label>("ratio");
        income = content.Q<Label>("income");
        totalSpend = content.Q<Label>("totalSpend");
        companionLore = content.Q<Label>("companionLore");
    }

    private void UpdateUI()
    {
        wallet.text = PlayerWallet.Wallet.ToString();
        ratio.text = PlayerWallet.IncomeRatio.ToString();
        income.text = PlayerWallet.MoneyIncome.ToString();
        totalSpend.text = PlayerWallet.TotalMoneySpend.ToString();
    }

    private void UpdateCompanionLore(Area areaData) => companionLore.text = areaData.CompanionLore;
}
