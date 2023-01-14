using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    /// <summary>
    /// Amount of money the player has.
    /// </summary>
    public static int Wallet
    {
        get { return wallet; }
    }

    /// <summary>
    /// IDLE money income in seconds.
    /// </summary>
    public static float MoneyIncome
    {
        get { return moneyIncome; }
    }

    /// <summary>
    /// All the money the player has spend.
    /// </summary>
    public static int TotalMoneySpend
    {
        get { return totalMoneySpend; }
    }

    /// <summary>
    /// Gol per click this wallet receives.
    /// </summary>
    public static int GoldPerClick
    {
        get { return goldPerClick; }
    }

    // Called when something changes inside the wallet.
    public static Action WalletUpdated;

    private static int wallet = 0;
    private static int moneyIncome = 0; // Amount of money the player gets when reached incomeRatio.
    private static int totalMoneySpend = 0;
    private static int goldPerClick = 1;
    private float nextIncomeTime;

    private void Update()
    {
        GenerateMoney();
    }

    public static void AddMoney(int amount)
    {
        wallet += amount;
        WalletUpdated?.Invoke();
    }

    public static void LoadWalletData(int data) => wallet = data;

    public static bool SpendMoney(int spendAmount)
    {
        // If we don't have enought money, we return false.
        if (wallet - spendAmount < 0) return false;
        // If we have the money, return true and update wallet size.
        else
        {
            wallet -= spendAmount;
            totalMoneySpend += spendAmount;
            WalletUpdated?.Invoke();
            return true;
        }
    }

    public static void IncreaseMoneyIncome(int moneyIncomeIncrease)
    {
        moneyIncome += moneyIncomeIncrease;
        WalletUpdated?.Invoke();
    }

    public static void IncreaseGoldPerClick(int goldIncrease) => goldPerClick += goldIncrease;

    /// <summary>
    /// Automaticaly adds money to the wallet depending on the income factor.
    /// </summary>
    private void GenerateMoney()
    {
        if (nextIncomeTime <= 0)
        {
            AddMoney(moneyIncome);
            nextIncomeTime = 1;
        }
        nextIncomeTime -= Time.deltaTime;
    }
}
