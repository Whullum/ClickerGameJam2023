using UnityEngine;

public class PlayerRevolver : MonoBehaviour
{
    /// <summary>
    /// Damage the player does in one click.
    /// </summary>
    public static int Damage
    {
        get { return damage; }
    }

    public static int PassiveDamage
    {
        get { return passiveDamage; }
    }

    private static int damage = 1;
    private static int passiveDamage = 0;

    public static void UpgradeDamage(int addAmount) => damage += addAmount;

    public static void UpgradePassiveDamage(int addAmount) => passiveDamage += addAmount;
}
