using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SoundUI : MonoBehaviour
{
    [SerializeField] Button toggleSoundButton;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite soundOff;
    public AudioMixer audioMixer;

    private void Start()
    {
        UpdateSoundIcon();
    }

    public void ToggleSound()
    {

        MusicManager.Get().ToggleMusicPlayingKey();
        UpdateSoundIcon();

        if (PlayerPrefs.GetInt(MusicManager.Get().GetMusicPlayingKey()) == 0) // Music IS muted
        {
            audioMixer.SetFloat("BackgroundVolume", -80f); // Mute background sound
        }
        else
        {
            audioMixer.SetFloat("BackgroundVolume", 0f); // Restore background sound
        }
    }


    private void UpdateSoundIcon()
    {
        if (PlayerPrefs.GetInt(MusicManager.Get().GetMusicPlayingKey()) == 0) // Music IS muted
        {
            toggleSoundButton.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            toggleSoundButton.GetComponent<Image>().sprite = soundOn;
        }
    }

}