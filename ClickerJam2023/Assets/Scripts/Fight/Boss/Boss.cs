using UnityEngine;

[CreateAssetMenu(fileName = "New Boss", menuName = "Fight/Boss")]
public class Boss : ScriptableObject
{
    public int ID;
    public float MaxHealth;
    public float HealthRegenRatio;
    public float ActiveTime;
    public string Name;
    [TextArea(5, 50)]
    public string Lore;
    public int BountyReward;
}
