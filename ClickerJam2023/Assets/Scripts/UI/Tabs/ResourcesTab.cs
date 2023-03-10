using UnityEngine;
using UnityEngine.UIElements;

public class ResourcesTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;
    private VisualElement goldImageClick;
    private Label wallet;
    private Label income;
    private Label idleIncome;
    private Label damage;
    private Label idleDamage;
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
        idleIncome = content.Q<Label>("idleIncome");
        damage = content.Q<Label>("damage");
        idleDamage = content.Q<Label>("idleDamage");
        companionLore = content.Q<Label>("companionLore");

        goldImageClick.RegisterCallback<MouseDownEvent>(GoldClick);
    }

    private void UpdateUI()
    {
        wallet.text = PlayerWallet.Wallet.ToString();
        income.text = PlayerWallet.GoldPerClick.ToString();
        idleIncome.text = PlayerWallet.MoneyIncome.ToString();
        damage.text = PlayerRevolver.Damage.ToString();
        idleDamage.text = PlayerRevolver.PassiveDamage.ToString();
    }

    private void UpdateCompanionLore(Area areaData) => companionLore.text = areaData.CompanionLore;

    private void GoldClick(MouseDownEvent evt)
    {
        PlayerWallet.AddMoney(PlayerWallet.GoldPerClick);
    }
}
