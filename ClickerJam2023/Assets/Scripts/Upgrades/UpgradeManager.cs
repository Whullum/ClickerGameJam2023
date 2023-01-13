using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeData[] LoadedUpgradeData {get;set;}

    private static Dictionary<string, Upgrade> allUpgrades = new Dictionary<string, Upgrade>();

    private void Awake()
    {
        LoadUpgradeScriptables();
        LoadUpgradesData();
    }

    private void OnEnable()
    {
        UpgradeTab.UpgradeButtonClick += UpgradeItem;
    }

    private void OnDisable()
    {
        UpgradeTab.UpgradeButtonClick -= UpgradeItem;
    }

    private void LoadUpgradeScriptables()
    {
        var upgrades = Resources.LoadAll<Upgrade>("Upgrades");

        for (int i = 0; i < upgrades.Length; i++)
        {
            allUpgrades.Add(upgrades[i].GUID, upgrades[i]);
        }
    }

    private void LoadUpgradesData()
    {
        UpgradeData[] upgrades = LoadedUpgradeData;

        for (int i = 0; i < upgrades.Length; i++)
        {
            Upgrade setUpgrade = allUpgrades[upgrades[i].GUID];

            setUpgrade.Cost = upgrades[i].Cost;
            setUpgrade.Value = upgrades[i].Value;
            setUpgrade.TimesUnlocked = upgrades[i].TimesUnlocked;
            setUpgrade.Unlocked = upgrades[i].Unlocked;

            ExpandSystems(setUpgrade, 0);
        }
    }

    public void UpgradeItem(string guid)
    {
        Upgrade upgrade = allUpgrades[guid];

        // If we don't have enough money, we cannot continue.
        if (!PlayerWallet.SpendMoney(upgrade.Cost)) return;

        int previousValue = upgrade.Value;
        int newCost = Mathf.CeilToInt(upgrade.Cost * upgrade.CostIncreaseRatio + upgrade.Cost);
        int newValue = Mathf.CeilToInt((upgrade.BaseValueUpgrade * upgrade.EffectIncreaseRatio) / 100 + upgrade.Value);

        upgrade.Cost = newCost;
        upgrade.Value = newValue;
        upgrade.TimesUnlocked++;

        if (upgrade.TimesUnlocked >= upgrade.NextUpgradeUnlockCount)
            if (upgrade.NextUpgrade != null)
                UnlockUpgrade(upgrade.NextUpgrade);

        ExpandSystems(upgrade, previousValue);
    }

    private void ExpandSystems(Upgrade upgrade, int previousValue)
    {
        switch (upgrade.Type)
        {
            case UpgradeType.Revolver_Damage:
                PlayerRevolver.UpgradeDamage(upgrade.Value - previousValue);
                break;
            case UpgradeType.IDLE_Revolver_Damage:
                PlayerRevolver.UpgradeIDLEDamage(upgrade.Value - previousValue);
                break;
            case UpgradeType.Gold_Collection:
                PlayerWallet.IncreaseGoldPerClick(upgrade.Value - previousValue);
                break;
            case UpgradeType.IDLE_Gold_Collection:
                PlayerWallet.IncreaseMoneyIncome(upgrade.Value - previousValue);
                break;
        }
    }

    private void UnlockUpgrade(Upgrade upgradeToUnlock)
    {
        upgradeToUnlock.Unlocked = true;
    }

    public static Upgrade GetUpgrade(string upgradeGuid)
    {
        foreach (KeyValuePair<string, Upgrade> item in allUpgrades)
        {
            if (item.Key == upgradeGuid)
                return item.Value;
        }
        return null;
    }
}
