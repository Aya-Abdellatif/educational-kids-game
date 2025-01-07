using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [HideInInspector]public static MusicManager Instance { get; set; }
    private const string MUSIC_PLAYING_KEY = "MusicPlayingKey";

    public AudioMixer audioMixer;

    [SerializeField] AudioSource SFXSource;
    public AudioClip buttonClickSound;
    public AudioClip correctAnswerSound;
    public AudioClip wrongAnswerSound;
    public AudioClip endGame;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicPlayingKey") == 0) // Music IS muted
        {
            audioMixer.SetFloat("BackgroundVolume", -80f); // Set volume to very low.
        }
        else
        {
            audioMixer.SetFloat("BackgroundVolume", 0f); // Restore normal volume.
        }
    }

    public void ToggleMusicPlayingKey()
    {
        if (PlayerPrefs.GetInt(MUSIC_PLAYING_KEY) == 0) // Music IS muted
        {
            PlayerPrefs.SetInt(MUSIC_PLAYING_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(MUSIC_PLAYING_KEY, 0);
        }
    }

    public string GetMusicPlayingKey()
    {
        return MUSIC_PLAYING_KEY;
    }

    public static MusicManager Get()
    {
        return Instance;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}