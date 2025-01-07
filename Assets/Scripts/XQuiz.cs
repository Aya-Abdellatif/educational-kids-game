using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XQuiz : MonoBehaviour {
    [System.Serializable]
    public class Question
    {
        public Sprite xImage;
        public Sprite[] choices;
        public int correctAnswerIndex;
    }

    public Sprite[] xImages;       // Array of number images (1 to 10)
    public Sprite[] questionImages;       // Array of number images (1 to 10)
    public Image questionImage;         // UI Image for the question
    public Image[] choiceButtons;       // Buttons for the choices

    public GameObject startPanel;       // Panel containing the start button
    public GameObject quizPanel;        // Panel containing the quiz
    public GameObject retakePanel; // Retake button panel
    public GameObject counterPanel;
    public Text scoreText;              // Text for displaying the score
    public Text resultText;             // Text for displaying the result

    private int currentScore = 0;
    private int totalQuestions = 5;
    private int currentQuestionIndex = 0;
    //private int minimumScore = 30;

    public Text countdownText;          // Text for the countdown display
    private int countdownTime = 3;      // Duration of the countdown (in seconds)


    private Question currentQuestion;

    public AudioClip countdownBeep;  // Sound for numbers

    public Slider quizTimerSlider;  // Reference to the Timer Slider
    public float totalQuizTime = 60f;  // Total duration for the quiz (in seconds)
    private float timeRemaining;

    private bool isQuizRunning = false;

    public Image characterImage;
    public Sprite[] characterSprites;

    private List<int> availableQuestions; // To track available question indices

    void Start()
    {
        quizTimerSlider.gameObject.SetActive(false); // Hide slider initially
        startPanel.SetActive(true);
        quizPanel.SetActive(false);
        retakePanel.SetActive(false);
        counterPanel.SetActive(false);
        countdownText.gameObject.SetActive(false); // Hide countdown initially
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
        for (int i = 0; i < characterSprites.Length; i++)
        {
            if (characterSprites[i].name == selectedCharacter)
            {
                characterImage.sprite = characterSprites[i];
                break;
            }
        }

        // Initialize the available questions list
        availableQuestions = new List<int>();
        for (int i = 0; i < xImages.Length; i++)
        {
            availableQuestions.Add(i);
        }
    }
    public void StartQuiz()
    {

        StartCoroutine(CountdownAndStart());
        currentScore = 0;
        currentQuestionIndex = 0;
        startPanel.SetActive(false);
        retakePanel.SetActive(false);
        counterPanel.SetActive(true);
        //quizPanel.SetActive(true);
        //DisplayNextQuestion();

    }
    void Update()
    {
        if (isQuizRunning)
        {
            timeRemaining -= Time.deltaTime;
            quizTimerSlider.value = timeRemaining;

            if (timeRemaining <= 0)
            {
                isQuizRunning = false;
                EndQuiz();
            }
        }
    }
    private IEnumerator CountdownAndStart()
    {
        //counterPanel.SetActive(true);
        countdownText.gameObject.SetActive(true);
        CanvasGroup canvasGroup = countdownText.GetComponent<CanvasGroup>();
        GetComponent<AudioSource>().PlayOneShot(countdownBeep);
        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return StartCoroutine(FadeInAndOut(canvasGroup, 0.7f));
        }

        countdownText.text = "Go!";
        countdownText.transform.localScale = Vector3.one * 2;
        LeanTween.scale(countdownText.gameObject, Vector3.one, 0.5f).setEaseOutBounce();
        yield return new WaitForSeconds(0.5f);

        // Start the quiz timer
        quizTimerSlider.gameObject.SetActive(true);
        //quizPanel.SetActive(true);
        timeRemaining = totalQuizTime;
        quizTimerSlider.maxValue = totalQuizTime;
        quizTimerSlider.value = totalQuizTime;
        isQuizRunning = true;

        quizPanel.SetActive(true);
        DisplayNextQuestion();
    }

    private IEnumerator FadeInAndOut(CanvasGroup canvasGroup, float duration)
    {
        float fadeSpeed = 2 / duration;

        // Fade In
        for (float t = 0; t <= 1; t += Time.deltaTime * fadeSpeed)
        {
            canvasGroup.alpha = t;
            yield return null;
        }

        // Hold for a moment
        yield return new WaitForSeconds(duration * 0.3f);

        // Fade Out
        for (float t = 1; t >= 0; t -= Time.deltaTime * fadeSpeed)
        {
            canvasGroup.alpha = t;
            yield return null;
        }
    }

    private void DisplayNextQuestion()
    {
        //Question question = new Question();

        if (currentQuestionIndex < totalQuestions)
        {
            currentQuestion = GenerateQuestion();
            //questionImage = currentQuestion.xImage;

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                choiceButtons[i].sprite = currentQuestion.choices[i];
                int choiceIndex = i; // Capture index for the button
                choiceButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                choiceButtons[i].GetComponent<Button>().onClick.AddListener(() => CheckAnswer(choiceIndex));
            }

            currentQuestionIndex++;
        }
        else
        {
            EndQuiz();
        }
    }
    private Question GenerateQuestion()
    {
        Question question = new Question();

        if (availableQuestions.Count == 0)
        {
            Debug.LogWarning("No more questions available!");
            return question;
        }

        int randomIndex = Random.Range(0, availableQuestions.Count);
        // Select a random number for the question
        int questionNumber = Random.Range(0, xImages.Length);
        availableQuestions.RemoveAt(randomIndex); // Remove the used question

        questionImage.sprite = questionImages[questionNumber];

        List<int> usedNumbers = new List<int> { questionNumber };

        question.choices = new Sprite[3];

        for (int i = 0; i < 3; i++)
        {
            int choiceNumber;
            do
            {
                choiceNumber = Random.Range(0, xImages.Length);
            } while (usedNumbers.Contains(choiceNumber));

            usedNumbers.Add(choiceNumber);
            question.choices[i] = xImages[choiceNumber];
        }

        // Set the correct answer at a random position
        question.correctAnswerIndex = Random.Range(0, 3);
        question.choices[question.correctAnswerIndex] = xImages[questionNumber];

       
        return question;
    }
    private void CheckAnswer(int choiceIndex)
    {
        if (choiceIndex == currentQuestion.correctAnswerIndex)
        {
            currentScore += 10;
            MusicManager.Instance.PlaySFX(MusicManager.Instance.correctAnswerSound);
        }
        else
        {
            MusicManager.Instance.PlaySFX(MusicManager.Instance.wrongAnswerSound);
        }
        scoreText.text = currentScore.ToString();
        DisplayNextQuestion();
    }
    private void EndQuiz()
    {
        quizTimerSlider.gameObject.SetActive(false);
        quizPanel.SetActive(false);
        //startPanel.SetActive(true);
        retakePanel.SetActive(true);
        if (currentScore < 30)
        {
            resultText.color = Color.red;
            resultText.text = "You can retake the quiz! Final Score: "+ currentScore;
            Debug.Log("Fail");
        }
        else
        {
            resultText.color = Color.green;
            resultText.text = "Congratulations! Final Score: "+ currentScore;
            Debug.Log("Success");
        }
        Debug.Log("Quiz Ended. Final Score: " + currentScore);
        MusicManager.Instance.PlaySFX(MusicManager.Instance.endGame);
    }
}