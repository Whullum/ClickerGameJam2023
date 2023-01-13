using UnityEngine;
using UnityEngine.UIElements;

public class ResourcesTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;
    private VisualElement goldImageClick;
    private Label wallet;
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
        goldImageClick = content.Q<VisualElement>("goldImageClick");
        wallet = content.Q<Label>("wallet");
        income = content.Q<Label>("income");
        totalSpend = content.Q<Label>("totalSpend");
        companionLore = content.Q<Label>("companionLore");

        goldImageClick.RegisterCallback<MouseDownEvent>(GoldClick);
    }

    private void UpdateUI()
    {
        wallet.text = PlayerWallet.Wallet.ToString();
        income.text = PlayerWallet.MoneyIncome.ToString();
        totalSpend.text = PlayerWallet.TotalMoneySpend.ToString();
    }

    private void UpdateCompanionLore(Area areaData) => companionLore.text = areaData.CompanionLore;

    private void GoldClick(MouseDownEvent evt)
    {
        PlayerWallet.AddMoney(PlayerWallet.GoldPerClick);
    }
}
