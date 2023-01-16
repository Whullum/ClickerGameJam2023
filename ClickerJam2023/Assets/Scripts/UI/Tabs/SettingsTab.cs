using UnityEngine;
using UnityEngine.UIElements;

public class SettingsTab : MonoBehaviour
{
    public FMOD.Studio.Bus masterBus;
    private string masterBusString = "bus:/";

    public FMOD.Studio.Bus musicBus;
    private string musicBusString = "bus:/Music";

    public FMOD.Studio.Bus soundBus;
    private string soundBusString = "bus:/Sound Effects";

    private VisualElement root;
    private VisualElement content;
    private Slider masterSlider;
    private Slider musicSlider;
    private Slider sfxSlider;
    private Button newGameButton;

    private void Awake()
    {
        InitializeDocument();
    }

    private void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.getVolume(out float volumeMaster);
        masterSlider.value = volumeMaster;

        musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
        musicBus.getVolume(out float volumeMusic);
        musicSlider.value = volumeMusic;

        soundBus = FMODUnity.RuntimeManager.GetBus(soundBusString);
        soundBus.getVolume(out float volumeSound);
        sfxSlider.value = volumeSound;
    }

    private void InitializeDocument()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("SettingsTab");
        content = root.Q<VisualElement>("container");
        masterSlider = content.Q<Slider>("masterSlider");
        musicSlider = content.Q<Slider>("musicSlider");
        sfxSlider = content.Q<Slider>("sfxSlider");
        newGameButton = content.Q<Button>("newGameButton");

        masterSlider.RegisterValueChangedCallback(MasterVolumeChanged);
        musicSlider.RegisterValueChangedCallback(MusicVolumeChanged);
        sfxSlider.RegisterValueChangedCallback(SFXVolumeChanged);

        newGameButton.clicked += NewGame;
    }

    private void MasterVolumeChanged(ChangeEvent<float> evt)
    {
        masterBus.setVolume(masterSlider.value);
    }

    private void MusicVolumeChanged(ChangeEvent<float> evt)
    {
        musicBus.setVolume(musicSlider.value);
    }

    private void SFXVolumeChanged(ChangeEvent<float> evt)
    {
        soundBus.setVolume(sfxSlider.value);
    }

    private void NewGame()
    {
        GameManager.Instance.NewGame();
    }
}
