using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour
{
    /// <summary>
    /// Damage the player does in one click.
    /// </summary>
    public static float Damage
    {
        get { return damage; }
    }

    private static float damage = 1;
}
