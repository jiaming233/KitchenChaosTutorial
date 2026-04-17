using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private const string SOUNDMANAGER_VOLUME = "SoundManagerVolume";

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 5f;

    private void Awake()
    {
        Instance = this;
        LoadVolume();
    }

    private void Start()
    {
        OrderManager.Instance.OnOrderSuccess += OrderManager_OnOrderSuccess;
        OrderManager.Instance.OnOrderFailed += OrderManager_OnOrderFailed;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickup += KitchenObjectHolder_OnPickup;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.trash);
    }

    private void KitchenObjectHolder_OnPickup(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup);
    }

    private void KitchenObjectHolder_OnDrop(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectDrop);
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.chop);
    }

    private void OrderManager_OnOrderFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail);
    }

    private void OrderManager_OnOrderSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess);
    }

    public void PlayFootstepSound(float volumeMultiplier = 0.3f)
    {
        PlaySound(audioClipRefsSO.footstep, volumeMultiplier);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning);
    }

    public void PlayWarningSound()
    {
        PlaySound(audioClipRefsSO.warning);
    }

    private void PlaySound(AudioClip[] audioClips, float volumeMultiplier = 0.2f)
    {
        PlaySound(audioClips, Camera.main.transform.position, volumeMultiplier);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 0.2f)
    {
        if (volume == 0)
            return;
        int index = Random.Range(0, audioClips.Length);
        AudioSource.PlayClipAtPoint(audioClips[index], position, volumeMultiplier * (volume / 10.0f));
    }


    public void ChangeVolume()
    {
        this.volume++;
        if(this.volume > 10)
        {
            this.volume = 0;
        }
        SaveVolume();
    }

    public float GetVolume()
    {
        return this.volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(SOUNDMANAGER_VOLUME, volume);
    }
    private void LoadVolume()
    {
        volume = PlayerPrefs.GetFloat(SOUNDMANAGER_VOLUME, volume);
    }
}
