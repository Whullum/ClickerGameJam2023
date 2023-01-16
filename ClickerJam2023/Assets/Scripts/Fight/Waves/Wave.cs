using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Fight/Wave")]
public class Wave : ScriptableObject
{
    public string WaveName;
    public int EnemyCount;
    public int MinEnemyHealth;
    public int MaxEnemyHealth;
    public int MinGoldPerKill;
    public int MaxGoldPerKill;
    public Texture2D EnemySprite;
}
