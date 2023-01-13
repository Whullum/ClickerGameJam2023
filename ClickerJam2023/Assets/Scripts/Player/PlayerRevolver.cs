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

    public static int IDLEDamage
    {
        get { return idleDamage; }
    }

    private static int damage = 1;
    private static int idleDamage;

    public static void UpgradeDamage(int addAmount) => damage += addAmount;

    public static void UpgradeIDLEDamage(int addAmount) => idleDamage += addAmount;
}
