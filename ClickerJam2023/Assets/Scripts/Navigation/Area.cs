using UnityEngine;

[CreateAssetMenu(fileName = "New Area", menuName = "Navigation / Area")]
public class Area : ScriptableObject
{
    public int ID;
    public string Name;
    public bool Unlocked; // For testing purposes until the Save System is implemented.
    [TextArea(5, 100)]
    public string Lore;
    [TextArea(5, 100)]
    public string CompanionLore;
    public BossEnemy BossPrefab;
    public Wave Wave;
    public Sprite BackgroundImage;
    public int musicModeID;
}
