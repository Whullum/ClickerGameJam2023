using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider masterBusVolumeSlider;
    public Slider musicBusVolumeSlider;
    public Slider soundBusVolumeSlider;

    public FMOD.Studio.Bus masterBus;
    private string masterBusString = "bus:/";

    public FMOD.Studio.Bus musicBus;
    private string musicBusString = "bus:/Music";

    public FMOD.Studio.Bus soundBus;
    private string soundBusString = "bus:/Sound Effects";

    private void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.getVolume(out float volumeMaster);
        masterBusVolumeSlider.value = volumeMaster;

        musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
        musicBus.getVolume(out float volumeMusic);
        musicBusVolumeSlider.value = volumeMusic;

        soundBus = FMODUnity.RuntimeManager.GetBus(soundBusString);
        soundBus.getVolume(out float volumeSound);
        soundBusVolumeSlider.value = volumeSound;
    }

    public void SetMasterVolume()
    {
        masterBus.setVolume(masterBusVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        musicBus.setVolume(musicBusVolumeSlider.value);
    }

    public void SetSoundVolume()
    {
        soundBus.setVolume(soundBusVolumeSlider.value);
    }
}
