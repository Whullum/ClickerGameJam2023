using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Fight/Boss")]
public class Boss : ScriptableObject
{
    public int ID;
    public int MaxHealth;
    public int HealthRegenRatio;
    public float ActiveTime;
    public string Name;
    [TextArea(5, 50)]
    public string Lore;
    public int BountyReward;
}
