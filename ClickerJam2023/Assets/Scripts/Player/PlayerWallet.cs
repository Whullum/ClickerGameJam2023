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
    /// Time to add to the wallet the current moneyIncome.
    /// </summary>
    public static float IncomeRatio
    {
        get { return incomeRatio; }
    }

    /// <summary>
    /// All the money the player has spend.
    /// </summary>
    public static int TotalMoneySpend
    {
        get { return totalMoneySpend; }
    }

    // Called when something changes inside the wallet.
    public static Action WalletUpdated;

    private static int wallet = 0;
    private static int moneyIncome = 0; // Amount of money the player gets when reached incomeRatio.
    private static int totalMoneySpend = 0;
    private static float incomeRatio = Mathf.Infinity; // Each ratio income comes.
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

    public static void IncreaseMoneyIncome(int moneyIncomeIncrease, float ratioIncrease)
    {
        moneyIncome += moneyIncomeIncrease;
        incomeRatio += ratioIncrease;
        WalletUpdated?.Invoke();
    }

    /// <summary>
    /// Automaticaly adds money to the wallet depending on the income factor.
    /// </summary>
    private void GenerateMoney()
    {
        if (nextIncomeTime >= incomeRatio)
        {
            wallet += moneyIncome;
            nextIncomeTime = 0;
            WalletUpdated?.Invoke();
        }

        nextIncomeTime += Time.deltaTime;
    }
}
