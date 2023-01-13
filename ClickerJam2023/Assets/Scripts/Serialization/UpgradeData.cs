[System.Serializable]
public class UpgradeData
{
    public string GUID;
    public int Cost;
    public int Value;
    public int TimesUnlocked;
    public bool Unlocked;

    public UpgradeData(string guid, int cost, int value, int timesUnlocked, bool unlocked)
    {
        GUID = guid;
        Cost = cost;
        Value = value;
        TimesUnlocked = timesUnlocked;
        Unlocked = unlocked;
    }
}
