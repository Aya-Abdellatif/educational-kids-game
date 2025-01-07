using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LetterManager : MonoBehaviour {

    public GameObject popUpPanel;
    public Image letter;
    public Text exampleText;
    public Image exampleImage;
    public AudioSource audioSource;
    public Button nextButton, previousButton, closeButton, soundButton;

    private int currentLetterIndex = 0;

    public Sprite[] letters;
    private string[] examples = { "is for apple", "is for ball", "is for cat", "is for dog", "is for elephant", "is for flower", "is for giraffe", "is for hat", "is for ice cream", "is for juice", "is for kiwi", "is for lion", "is for monkey", "is for net", "is for orange", "is for pizza", "is for queen", "is for rabbit", "is for star", "is for tomato", "is for unicorn", "is for van", "is for watermelon", "is for xyelephone", "is for yoyo", "is for zebra" }; // Example words
    public Sprite[] exampleImages;
    public AudioClip[] letterSounds;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        nextButton.onClick.AddListener(ShowNextLetter);
        previousButton.onClick.AddListener(ShowPreviousLetter);
        closeButton.onClick.AddListener(ClosePopUp);
        soundButton.onClick.AddListener(RepeatLetterSound);

        //Debug.Log("audioSource: " + audioSource);
        //Debug.Log("letterSounds: " + letterSounds);
        //Debug.Log("letterSounds.Length: " + letterSounds.Length);
    }
    public void OnLetterClick(int letterIndex)
    {
        currentLetterIndex = letterIndex;
        Debug.Log("OnLetterClick triggered for letter index: " + letterIndex);

        UpdatePopUpContent();
        popUpPanel.SetActive(true);
    }

    private void UpdatePopUpContent()
    {
        if (letters != null && letters.Length > currentLetterIndex)
        {
            letter.sprite = letters[currentLetterIndex];
        }
        
        exampleText.text = examples[currentLetterIndex];
        
        if (exampleImages != null && currentLetterIndex < exampleImages.Length)
        {
            exampleImage.sprite = exampleImages[currentLetterIndex];
            exampleImage.gameObject.SetActive(true);
        }
        else
        {
            exampleImage.gameObject.SetActive(false);
        }

        PlayLetterSound();
    }
    private void PlayLetterSound()
    {
        if (audioSource != null && letterSounds != null && letterSounds.Length > currentLetterIndex)
        {
            audioSource.clip = letterSounds[currentLetterIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or LetterSounds is missing or not correctly assigned.");
        }
    }
    public void RepeatLetterSound()
    {
        PlayLetterSound();
    }
    private void ShowNextLetter()
    {
        currentLetterIndex = (currentLetterIndex + 1) % letters.Length; // Loop back to A if at Z
        UpdatePopUpContent();
    }
    private void ShowPreviousLetter()
    {
        currentLetterIndex = (currentLetterIndex - 1 + letters.Length) % letters.Length; // Loop to Z if at A
        UpdatePopUpContent();
    }
    private void ClosePopUp()
    {
        popUpPanel.SetActive(false);
    }
}
