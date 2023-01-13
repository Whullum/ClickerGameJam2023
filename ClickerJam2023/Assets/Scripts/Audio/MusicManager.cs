using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private FMOD.Studio.EventInstance musicInstance;

    // Start is called before the first frame update
    void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MusicContainer");

        musicInstance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE musicPlaybackState);
        if (musicPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            musicInstance.start();
        }
    }

    public void ChangeMusicState(int stateID)
    {
        // Change the music state to the corresponding area
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicState", stateID);
    }
}
