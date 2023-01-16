using UnityEngine;
using System.Collections;

public class PlayerRevolver : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    private static bool isClicking = false;
    public static bool IsClicking { get => isClicking; set => isClicking = value; }

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

    private void Start()
    {
        playerAnimator.SetBool("IsShooting", isClicking);
    }

    private void OnDisable()
    {
        damage = 1;
        passiveDamage = 0;
    }

    private void Update()
    {
        playerAnimator.SetBool("IsShooting", isClicking);

        if (isClicking)
        {
            StartCoroutine(AnimationCooldown());
        }
    }

    private IEnumerator AnimationCooldown()
    {
        yield return new WaitForSeconds(.5f);

        isClicking = false;
    }

    public static void UpgradeDamage(int addAmount) => damage += addAmount;

    public static void UpgradePassiveDamage(int addAmount) => passiveDamage += addAmount;
}
