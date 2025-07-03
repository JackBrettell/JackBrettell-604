using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
public class TutorialBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject tutorialObj;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private Image slideImage;
    [SerializeField] private TutorialInfo[] tutorialInfos;

    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject tutorialPrompt;
    private int currentStep = 0;

    private void Start()
    {
        StartTutorial();
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
            tutorialObj.SetActive(false);
            currentStep = 0;
            return;
        }
        UpdateTutorialSlide();
    }

    private void UpdateTutorialSlide()
    {
        if (currentStep < tutorialInfos.Length)
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
        else
        {
            EndTutorial();
        }
    }

    public void EndTutorial()
    {
        tutorialObj.SetActive(false);
        currentStep = 0;
    }
}

[System.Serializable]
public class TutorialInfo
{
    public string SlideText;
    public Sprite SlideImage;
}
