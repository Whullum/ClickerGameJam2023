using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string GUID;
    public int Cost;
    public int BaseValueUpgrade;
    public float EffectIncreaseRatio;
    public float CostIncreaseRatio;
    public Upgrade NextUpgrade;
    public int NextUpgradeUnlockCount;
    public int TimesUnlocked;
    public bool Unlocked;
    public UpgradeType Type;
    public int Value { get; set; }

    private void OnEnable()
    {
        Value = 0;
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(GUID))
        {
            GUID = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}
