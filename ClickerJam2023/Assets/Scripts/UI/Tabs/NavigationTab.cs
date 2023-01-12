using UnityEngine;
using UnityEngine.UIElements;

public class NavigationTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;

    private void Awake()
    {
        InitializeDocument();
    }

    private void Start()
    {
        CreateLevels();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("NavigationTab");
        content = root.Q<VisualElement>("content");
    }

    /// <summary>
    /// Creates one element for each area the game has. Used for navigation.
    /// </summary>
    private void CreateLevels()
    {
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

            content.Add(newArea);

            if (allAreas[i].Unlocked)
            {
                newArea.userData = allAreas[i].ID;
                newArea.AddToClassList("area");
                newArea.RegisterCallback<MouseDownEvent>(SelectArea);
                text.text = allAreas[i].Name;
            }
            else
            {
                newArea.AddToClassList("area-locked");
                text.text = "Locked";
            }
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

        AreaNavigation.NavigateArea(areaID);
    }
}
