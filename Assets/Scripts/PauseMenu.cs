using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject debugMenu;
    [SerializeField] private PlayerCameraController playerCameraController;
    [SerializeField] private HUD hud;
    public bool isPaused = false;



    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) // Check if the button was pressed
        {
            Debug.Log("Pause button pressed");
            TogglePauseMenu();
        }
    }
    
    // Toggle pause menu
    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerCameraController.enabled = false;
        }
        else
        {
            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerCameraController.enabled = true;

        }
    }

    public void OnResume()
    {
        TogglePauseMenu();
    }

    public void OnQuitToDesktop()
    {
        Application.Quit();
    }
    public void OnQuitToMenu()
    {
        // load main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void OnDebugMenu()
    {
        debugMenu.SetActive(!debugMenu.activeSelf);
    }
    public void OnPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");

    }
}
