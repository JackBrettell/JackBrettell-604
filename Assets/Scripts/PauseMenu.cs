using UnityEngine;
using UnityEngine.InputSystem;

// Manages the game's pause menu, including pausing/resuming gameplay,
// handling debug and settings menus, and scene transitions.

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject debugMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private PlayerCameraController playerCameraController;
    [SerializeField] private HUD hud;
    public bool isPaused = false;

    private bool isDebugOpen = false;
    private bool isSettingsOpen = false;


    // Handles the pause input action.
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) // Check if the button was pressed
        {
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

    public void OnPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");

    }

    public void OnDebugMenu()
    {
        ToggleMenu(ref isDebugOpen, debugMenu, ref isSettingsOpen, settingsMenu);
    }

    public void OnSettingsMenu()
    {
        ToggleMenu(ref isSettingsOpen, settingsMenu, ref isDebugOpen, debugMenu);
    }

    private void ToggleMenu(ref bool currentMenuState, GameObject currentMenu, ref bool otherMenuState, GameObject otherMenu)
    {
        if (otherMenuState)
        {
            otherMenuState = false;
            otherMenu.SetActive(false);
            currentMenu.SetActive(!currentMenu.activeSelf);
            currentMenuState = currentMenu.activeSelf;
        }
        else
        {
            currentMenuState = !currentMenuState;
            currentMenu.SetActive(currentMenuState);
        }
    }

}
