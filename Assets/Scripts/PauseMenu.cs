using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
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
        // Load the main menu scene
    }
}
