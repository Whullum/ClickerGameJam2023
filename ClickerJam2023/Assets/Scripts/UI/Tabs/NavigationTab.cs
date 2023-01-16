using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static System.TimeZoneInfo;

public class NavigationTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;
    private VisualElement navigationProgressBar;
    private VisualElement navigationProgress;
    private VisualElement buttonsContainer;
    private Label navigationText;
    private bool loadingArea;
    private float transitionTime;
    [SerializeField] private float areaTransitionTime = 2;

    [SerializeField]
    private MusicManager musicManager;

    private void Awake()
    {
        InitializeDocument();
    }

    private void OnEnable()
    {
        AreaNavigation.AreaLoaded += CreateLevels;
    }

    private void OnDisable()
    {
        AreaNavigation.AreaLoaded -= CreateLevels;
    }

    private void Start()
    {
        CreateLevels(new Area());
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("NavigationTab");
        content = root.Q<VisualElement>("content");
        navigationProgressBar = root.Q<VisualElement>("navigationProgressBar");
        buttonsContainer = root.Q<VisualElement>("buttonsContainer");
        navigationProgress = root.Q<VisualElement>("progress");
        navigationText = root.Q<Label>("navigationText");
    }

    /// <summary>
    /// Creates one element for each area the game has. Used for navigation.
    /// </summary>
    private void CreateLevels(Area areaData)
    {
        buttonsContainer.Clear();

        Area[] allAreas = AreaNavigation.AllAreas;

        for (int i = 0; i < allAreas.Length; i++)
        {
            VisualElement newArea = new VisualElement();
            VisualElement textContainer = new VisualElement();
            Label text = new Label();

            textContainer.AddToClassList("text-container");
            text.AddToClassList("area-text");
            textContainer.Add(text);
            newArea.Add(textContainer);

            buttonsContainer.Add(newArea);

            if (allAreas[i].Unlocked)
            {
                newArea.userData = allAreas[i].ID;
                newArea.AddToClassList("area");
                newArea.RegisterCallback<MouseOverEvent>((type) => 
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Hover");
                });
                newArea.RegisterCallback<MouseDownEvent>(SelectArea);
                text.text = allAreas[i].Name;

                if (allAreas[i].Active)
                {
                    newArea.AddToClassList("area-active");
                }
                else
                {
                    newArea.RemoveFromClassList("area-active");
                }
                    

            }
            else
            {
                newArea.AddToClassList("area-locked");
                text.text = "Locked";
            }
            navigationProgressBar.BringToFront();
        }
    }

    /// <summary>
    /// Navigates to the selected area, loading all of its data.
    /// </summary>
    /// <param name="evt"></param>
    private void SelectArea(MouseDownEvent evt)
    {
        VisualElement areaButton = evt.currentTarget as VisualElement;
        int areaID = (int)areaButton.userData;

        StartCoroutine(LoadNewArea(areaID));
        loadingArea = true;
    }

    private IEnumerator LoadNewArea(int areaID)
    {
        if (loadingArea) yield break;

        navigationText.text = "Traveling to another dimension...";
        transitionTime = 0;

        while (transitionTime < areaTransitionTime)
        {
            transitionTime += Time.deltaTime;
            float transitionPercent = transitionTime / areaTransitionTime * 100;

            navigationProgress.style.width = Length.Percent(transitionPercent);

            yield return null;
        }

        navigationProgress.style.width = Length.Percent(0);
        navigationText.text = "You can travel to another area";
        loadingArea = false;
        AreaNavigation.NavigateArea(areaID);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Click");
    }
}