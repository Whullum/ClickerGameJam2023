using UnityEngine;

[CreateAssetMenu(fileName ="New Area", menuName ="Navigation / Area")]
public class Area : ScriptableObject
{
    public int ID;
    public string Name;
    [TextArea(5, 50)]
    public string Lore;
    public BossEnemy BossPrefab;
}
