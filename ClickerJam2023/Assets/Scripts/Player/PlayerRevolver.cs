using UnityEngine;
using System.Collections;

public class PlayerRevolver : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    private static bool isClicking = false;
    public static bool IsClicking { get => isClicking; set => isClicking = value; }

    private float baseAnimationTimer = .1f;
    private float currentAnimationTime;

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

    private void Start()
    {
        playerAnimator.SetBool("IsShooting", isClicking);
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

    public static void UpgradeIDLEDamage(int addAmount) => idleDamage += addAmount;
}
