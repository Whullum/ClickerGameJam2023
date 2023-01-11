using JetBrains.Annotations;
using System;
using UnityEngine;

public class AreaNavigation : MonoBehaviour
{
    public static Action<BossEnemy> AreaLoaded;

    private Area[] allAreas;
    private Area activeArea;
    [SerializeField] private int areaToLoad = 0; // For testing purposes.

    private void Awake()
    {
        LoadAreasScriptables();
    }

    private void Start()
    {
        NavigateArea(areaToLoad); // We start in the first area.
    }

    private void OnEnable()
    {
        BossManager.BossDefeated += UnlockNextArea;
    }

    private void OnDisable()
    {
        BossManager.BossDefeated -= UnlockNextArea;
    }

    private void NavigateArea(int areaID)
    {
        for(int i = 0; i < allAreas.Length; i++)
        {
            if (allAreas[i].ID == areaID)
            {
                activeArea = allAreas[i];
                AreaLoaded?.Invoke(activeArea.BossPrefab);
                return;
            }
        }
    }

    private void UnlockNextArea()
    {
        NavigateArea(activeArea.ID + 1);
    }

    private void LoadAreasScriptables()
    {
        allAreas = Resources.LoadAll<Area>("Navigation");
    }
}
