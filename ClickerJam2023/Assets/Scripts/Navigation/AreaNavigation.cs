using System;
using UnityEngine;

public class AreaNavigation : MonoBehaviour
{
    public static AreaData[] AreaData { get; set; }
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
    private ParticleSystem areaLoadEffect;

    private void Awake()
    {
        areaLoadEffect = GameObject.Find("AreaLoadedEffect").GetComponent<ParticleSystem>();

        LoadAreasScriptables();
        LoadAreaData();

        // For now we start on a predefined area. In the near future we need to load the last area the plaeyr was on.
        NavigateArea(activeArea.ID);
    }

    private void OnEnable()
    {
        BossManager.BossDefeated += UnlockNextArea;
        AreaLoaded += LoadAreaEnvironment;
    }

    private void OnDisable()
    {
        BossManager.BossDefeated -= UnlockNextArea;
        AreaLoaded -= LoadAreaEnvironment;
    }

    /// <summary>
    /// Navigates to another area.
    /// </summary>
    /// <param name="areaID">ID of the new area to load.</param>
    public static void NavigateArea(int areaID)
    {
        for (int i = 0; i < allAreas.Length; i++)
        {
            if (activeArea != null) activeArea.Active = false;

            if (allAreas[i].ID == areaID)
            {
                // This area is now the active one.
                activeArea = allAreas[i];
                // Unlock this area if it was not yet unlocked.
                activeArea.Unlocked = true;
                activeArea.Active = true;
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
        // We iterate to the next avaliable area.
        NavigateArea(activeArea.ID + 1);

        areaLoadEffect.Play();

        FightManager.StartWave();
    }

    /// <summary>
    /// Load all the area scriptable objects.
    /// </summary>
    private void LoadAreasScriptables()
    {
        allAreas = Resources.LoadAll<Area>("Navigation");
    }

    private void LoadAreaData()
    {
        if (AreaData.Length <= 0) return;

        for (int i = 0; i < allAreas.Length; i++)
        {
            for (int j = 0; j < AreaData.Length; j++)
            {
                if (allAreas[i].ID == AreaData[j].ID)
                {
                    allAreas[i].Unlocked = AreaData[j].Unlocked;
                    allAreas[i].Active = AreaData[j].Active;
                    continue;
                }
            }
            if (allAreas[i].Active)
                activeArea = allAreas[i];
        }
    }

    private void LoadAreaEnvironment(Area areaData)
    {
        areaLoadEffect.Play();
    }
}
