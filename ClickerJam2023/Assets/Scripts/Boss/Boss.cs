using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Boss", menuName ="Boss/New Boss")]
public class Boss : ScriptableObject
{
    public int ID;
    public float MaxHealth;
    public float HealthRegenRatio;
    public string Name;
    [TextArea(5,50)]
    public string Lore;
}
