using System;
using UnityEngine;

public class AreaNavigation : MonoBehaviour
{
    /// <summary>
    /// All the different areas teh game has.
    /// </summary>
    public static Area[] AllAreas
    {
        get { return allAreas; }
    }

    // Invoked when the player selects a different area to load.
    public static Action<Area> AreaLoaded;

    private static Area[] allAreas;
    // Current active area.
    private static Area activeArea;

    [SerializeField] private int areaToLoad = 0; // For testing purposes.

    private void Awake()
    {
        LoadAreasScriptables();
    }

    private void Start()
    {
        // For now we start on a predefined area. In the near future we need to load the last area the plaeyr was on.
        NavigateArea(areaToLoad);
    }

    private void OnEnable()
    {
        BossManager.BossDefeated += UnlockNextArea;
    }

    private void OnDisable()
    {
        BossManager.BossDefeated -= UnlockNextArea;
    }

    /// <summary>
    /// Navigates to another area.
    /// </summary>
    /// <param name="areaID">ID of the new area to load.</param>
    public static void NavigateArea(int areaID)
    {
        for (int i = 0; i < allAreas.Length; i++)
        {
            if (allAreas[i].ID == areaID)
            {
                // This area is now the active one.
                activeArea = allAreas[i];
                // Launch event with the boss prefab data to spawn. Subscribed by BossManager.
                AreaLoaded?.Invoke(activeArea);

                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", activeArea.musicModeID);
                return;
            }
        }
    }

    /// <summary>
    /// When the current boss is defeated, we unlock the next area if it is not yet unlocked.
    /// </summary>
    private void UnlockNextArea()
    {
        // For now we only iterate to the next avaliable area.
        NavigateArea(activeArea.ID + 1);
    }

    /// <summary>
    /// Load all the area scriptable objects.
    /// </summary>
    private void LoadAreasScriptables()
    {
        allAreas = Resources.LoadAll<Area>("Navigation");
    }
}
