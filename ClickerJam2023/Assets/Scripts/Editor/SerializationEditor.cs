using UnityEditor;

public class SerializationEditor
{
    [MenuItem("Utils/Create new base game data")]
    public static void ShowClear()
    {
        SerializationSystem.SaveInitialUpgradeData();
    }
}
