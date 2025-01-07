using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public Text characterNameText;
    public Image characterImage;
    public Sprite[] characterSprites;

    void Start()
    {
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
        characterNameText.text = "Hi, " + selectedCharacter + "\n   What do you want to learn?";

        for (int i = 0; i < characterSprites.Length; i++)
        {
            if (characterSprites[i].name == selectedCharacter)
            {
                characterImage.sprite = characterSprites[i];
                break;
            }
        }
    }
}
