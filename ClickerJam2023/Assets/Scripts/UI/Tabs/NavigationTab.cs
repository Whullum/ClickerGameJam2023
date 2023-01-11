using UnityEngine;
using UnityEngine.UIElements;

public class NavigationTab : MonoBehaviour
{
    private VisualElement root;
    private VisualElement content;
    private Area[] allAreas; // Testing purposes, delete after Save System is implemented.

    private void Awake()
    {
        LoadAreasScriptables();
        InitializeDocument();
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("NavigationTab");
        content = root.Q<VisualElement>("content");

        CreateLevels();
    }

    private void CreateLevels()
    {
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

    private void LoadAreasScriptables()
    {
        allAreas = Resources.LoadAll<Area>("Navigation");
    }

    private void SelectArea(MouseDownEvent evt)
    {
        VisualElement areaButton = evt.currentTarget as VisualElement;
        int areaID = (int)areaButton.userData;

        AreaNavigation.NavigateArea(areaID);
    }
}
