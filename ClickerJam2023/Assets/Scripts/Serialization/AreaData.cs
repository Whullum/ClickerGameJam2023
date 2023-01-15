[System.Serializable]
public class AreaData
{
    public int ID;
    public bool Unlocked;
    public bool Active;

    public AreaData(int ID, bool unlocked, bool active)
    {
        this.ID = ID;
        this.Unlocked = unlocked;
        Active = active;
    }
}
