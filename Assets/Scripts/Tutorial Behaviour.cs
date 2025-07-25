using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using static GameSaveData;

public class TutorialBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject tutorialObj;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private Image slideImage;
    [SerializeField] private TutorialInfo[] tutorialInfos;

    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject tutorialPrompt;
    [SerializeField] private GameObject[] uiElements;
    private int currentStep = 0;

    private void Start()
    {
        GameSaveData data = SaveSystem.LoadGame();

        // If no save exists open tutorial popup
        if (data == null)
        {
            StartTutorial();
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            foreach (var element in uiElements)
            {
                element.SetActive(false);
            }
        }

    }
    private void StartTutorial()
    {
        tutorialObj.SetActive(true);
        currentStep = 0; 
        UpdateTutorialSlide(); 
    }

    public void NextStep()
    { 
        currentStep++;
        if (currentStep >= tutorialInfos.Length)
        {
            EndTutorial();
            return;
        }
        UpdateTutorialSlide();
    }

    private void UpdateTutorialSlide()
    {

        if (currentStep != 0)
        {
            closeButton.SetActive(false);
            tutorialPrompt.SetActive(false);
        }

        var info = tutorialInfos[currentStep];
        bodyText.text = info.SlideText;
        if (slideImage != null && info.SlideImage != null)
        {
            slideImage.sprite = info.SlideImage;
            slideImage.gameObject.SetActive(true);
        }
        else if (slideImage != null)
        {
            slideImage.gameObject.SetActive(false);
        }

    }

    public void EndTutorial()
    {
        tutorialObj.SetActive(false);
        currentStep = 0;

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        // Enable UI
        foreach (var element in uiElements)
        {
            element.SetActive(true);
        }
    }
}

[System.Serializable]
public class TutorialInfo
{
    public string SlideText;
    public Sprite SlideImage;
}
