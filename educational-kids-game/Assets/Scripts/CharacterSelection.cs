using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public Button[] characterButtons;
    public Text selectedCharacterText;
    public string[] characterNames;
    public string nextSceneName = "what to learn scene";

    private string selectedCharacter;
    void Start()
    {
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => SelectCharacter(index));
        }
    }

    public void SelectCharacter(int index)
    {
        MusicManager.Instance.PlaySFX(MusicManager.Instance.buttonClickSound);
        selectedCharacter = characterNames[index];
        selectedCharacterText.text = selectedCharacter;
    }

    public void OnPlayButtonClick()
    {
        MusicManager.Instance.PlaySFX(MusicManager.Instance.buttonClickSound);

        if (!string.IsNullOrEmpty(selectedCharacter))
        {
            PlayerPrefs.SetString("SelectedCharacter", selectedCharacter);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No character selected!");
        }
    }
}
