using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour {

    public GameObject popUpPanel;
    public Image number;
    public Image exampleImage;
    public AudioSource audioSource;
    public Button nextButton, previousButton, closeButton, soundButton;

    private int currentNumberIndex = 0;

    public Sprite[] numbers;
    public Sprite[] exampleImages;
    public AudioClip[] numberSounds;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        nextButton.onClick.AddListener(ShowNextNumber);
        previousButton.onClick.AddListener(ShowPreviousNumber);
        closeButton.onClick.AddListener(ClosePopUp);
        soundButton.onClick.AddListener(RepeatNumberSound);
    }
    public void OnNumberClick(int numberIndex)
    {
        currentNumberIndex = numberIndex;
        //Debug.Log("OnNumberClick triggered for number index: " + numberIndex);

        UpdatePopUpContent();
        popUpPanel.SetActive(true);
    }

    private void UpdatePopUpContent()
    {
        if (numbers != null && numbers.Length > currentNumberIndex)
        {
            number.sprite = numbers[currentNumberIndex];
        }

        if (exampleImages != null && currentNumberIndex < exampleImages.Length)
        {
            exampleImage.sprite = exampleImages[currentNumberIndex];
            exampleImage.gameObject.SetActive(true);
        }
        else
        {
            exampleImage.gameObject.SetActive(false);
        }

        PlayNumberSound();
    }
    private void PlayNumberSound()
    {
        if (audioSource != null && numberSounds != null && numberSounds.Length > currentNumberIndex)
        {
            audioSource.clip = numberSounds[currentNumberIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or NumberSounds is missing or not correctly assigned.");
        }
    }
    public void RepeatNumberSound()
    {
        PlayNumberSound();
    }
    private void ShowNextNumber()
    {
        currentNumberIndex = (currentNumberIndex + 1) % numbers.Length; // Loop back to 1 if at 10
        UpdatePopUpContent();
    }

    private void ShowPreviousNumber()
    {
        currentNumberIndex = (currentNumberIndex - 1 + numbers.Length) % numbers.Length; // Loop to 10 if at 1
        UpdatePopUpContent();
    }

    private void ClosePopUp()
    {
        popUpPanel.SetActive(false);
    }

}
