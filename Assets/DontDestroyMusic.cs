using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    public AudioSource musicSource;    
    public Sprite musicOnSprite;       
    public Sprite musicOffSprite;      
    public GameObject musicButton;     
    private bool isMusicOn = true;      

    // Start is called before the first frame update
    void Start()
    {
        
        if (musicSource != null)
            musicSource.Play();
    }

    
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
        {
            musicSource.Play();   
            musicButton.GetComponent<UnityEngine.UI.Image>().sprite = musicOnSprite;  // تغيير الصورة إلى Music On
        }
        else
        {
            musicSource.Pause();  
            musicButton.GetComponent<UnityEngine.UI.Image>().sprite = musicOffSprite;  // تغيير الصورة إلى Music Off
        }
    }
}
