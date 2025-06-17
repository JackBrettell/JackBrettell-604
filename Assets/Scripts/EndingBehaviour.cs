using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float fadeAmount = 0.8f;
    [SerializeField] private EndingScene endingScene;

    [Header("===== Stats =====")]
    [Header("Title")]
    [SerializeField] private TMPro.TextMeshProUGUI endingType;
    [SerializeField] private float endingScreenDelay = 2f;


    [Header("Kills")]
    private int killCount = 0;

    [SerializeField] private TMPro.TextMeshProUGUI KillNumber;

    [Header("Rounds survived")]
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TMPro.TextMeshProUGUI roundNumber;


    [Header("Time survived")]
    [SerializeField] private TMPro.TextMeshProUGUI timeSurvivedText;
    [SerializeField] private float timeSurvivedDuration = 1f;
    private bool hasDied = false;
    private CanvasGroup canvasGroup;

    private bool hasEscaped = false;

    // If in Unity Editor K will kill the player
#if UNITY_EDITOR
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.K) && !hasDied)
        {
            HandlePlayerDeath(false);
            Debug.Log(killCount);
        }
        if (Input.GetKeyDown(KeyCode.L) && !hasDied)
        {
            HandlePlayerDeath(true);
        }
    }
#endif

    private void OnEnable()
    {
        playerHealth.OnDeath += HandlePlayerDeath;
        EnemyBase.OnAnyEnemyKilled += UpdateKillCount;
        endingScene.OnCarInteracted += HandlePlayerDeath; 

    }
    private void Start()
    {
        canvasGroup = deathScreen.GetComponent<CanvasGroup>();
        deathScreen.SetActive(false);

        StartCoroutine(TimeSurvived());
    }

    private void HandlePlayerDeath(bool hasEscaped)
    {
        if (hasEscaped)
        {
            endingType.text = "You escaped!";
            StartCoroutine(WaitForEndingScreen());
            return; 
        }
        else
        {
            endingType.text = "You died!";
            ShowDeathScreen();
        }
    }
    private void ShowDeathScreen()
    {
        hasDied = true;
        deathScreen.SetActive(true);
        canvasGroup.alpha = 0f;

        // Unlock and make cursor visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Fade in death screen
        canvasGroup.DOFade(fadeAmount, fadeDuration).SetEase(Ease.OutQuad);

        // Pause game once the menu has faded in
        StartCoroutine(PauseOnMenuAppear());

        // Set the round number
        int roundNum = 0;
        roundNumber.text = roundNum.ToString();
    }


    private IEnumerator PauseOnMenuAppear()
    {
        yield return new WaitForSeconds(fadeDuration);
        Time.timeScale = 0f;
    }


    private IEnumerator TimeSurvived()
    {
        while (!hasDied)
        {
            timeSurvivedDuration += Time.deltaTime;
            yield return null;
        }

        timeSurvivedText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeSurvivedDuration / 60), Mathf.FloorToInt(timeSurvivedDuration % 60));
    }

    private void UpdateKillCount()
    {
        killCount++;
        KillNumber.text = killCount.ToString();
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenue()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void OnDestroy()
    {
        playerHealth.OnDeath -= HandlePlayerDeath;

        EnemyBase.OnAnyEnemyKilled -= UpdateKillCount;
    }

    private IEnumerator WaitForEndingScreen()
    {
        yield return new WaitForSeconds(endingScreenDelay); // Wait for the delay
        ShowDeathScreen(); // Now show the death screen
    }

}
