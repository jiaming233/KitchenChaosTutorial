using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {  get; private set; }

    private const string MUSICMANAGER_VOLUME = "MusicManagerVolume";

    private AudioSource musicAudioSource;

    private float originalVolume;

    private float volume = 5;

    private void Awake()
    {
        Instance = this;
        LoadVolume();
    }

    // Start is called before the first frame update
    void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
        originalVolume = musicAudioSource.volume;

        UpdateVolume();
    }

    public void ChangeVolume()
    {
        volume++;
        if (volume > 10)
        {
            volume = 0;
        }
        SaveVolume();
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        musicAudioSource.enabled = volume > 0;
        musicAudioSource.volume = originalVolume * (volume / 10f);
    }

    public float GetVolume()
    {
        return volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(MUSICMANAGER_VOLUME, volume);
    }
    private void LoadVolume()
    {
        volume = PlayerPrefs.GetFloat(MUSICMANAGER_VOLUME, volume);
    }
}
