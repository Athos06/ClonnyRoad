using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager> {

    public AudioMixer masterMixer;

    private bool audioEnabled = true;
    public bool AudioEnabled
    {
        get
        {
            return audioEnabled;
        }
    }
   

    private void Awake()
    {
       
        DontDestroyOnLoad(this);

        
    }

    // Use this for initialization
    void Start () {
        LoadAudioPreferences();

    }
	
    private void LoadAudioPreferences()
    {
        int audioState = PlayerPrefs.GetInt(Saves.MusicOn);

        audioEnabled = audioState > 0 ? true : false;   

        float vol = audioEnabled ? 0 : -80;

        setAudioVol(vol);
    }

    public void ToggleAudio(bool toogle)
    {
        if (toogle == audioEnabled)
            return;

        audioEnabled = !audioEnabled;
        float vol = audioEnabled ? 0 : -80;
        setAudioVol(vol);

        if (audioEnabled)
            PlayerPrefs.SetInt(Saves.MusicOn, 1);
        else
            PlayerPrefs.SetInt(Saves.MusicOn, 0);

        PlayerPrefs.Save();
        Debug.Log("we save audioenabled to " + audioEnabled);
    }

    public void setAudioVol(float vol)
    {
        masterMixer.SetFloat("MasterVolume", vol);
    }
}
