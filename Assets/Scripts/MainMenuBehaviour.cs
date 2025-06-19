using UnityEngine;
using TMPro;
using NUnit.Framework.Interfaces;
using System;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject warningButton;

    private void Start()
    {
        // If a save exists give option to continue
        GameSaveData data = SaveSystem.LoadData();
        if (data != null)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

    }
    public void OnNewGameButtonClicked()
    {
        warningButton.SetActive(true);
    }

    public void OnContinueButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    // Warning panel
    public void OnNewGameCancel()
    {
        warningButton.SetActive(false);
    }

    public void OnNewGameConfirm()
    {
        SaveManager.DeleteSavedData();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

}
