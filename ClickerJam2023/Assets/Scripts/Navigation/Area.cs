using UnityEngine;

[CreateAssetMenu(fileName = "New Area", menuName = "Navigation / Area")]
public class Area : ScriptableObject
{
    public int ID;
    public string Name;
    public bool Unlocked; // For testing purposes until the Save System is implemented.
    [TextArea(5, 50)]
    public string Lore;
    public BossEnemy BossPrefab;
    public Sprite BackgroundImage;
}
