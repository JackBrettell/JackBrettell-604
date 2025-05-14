using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class Deathbehaviour : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float fadeAmount = 0.8f;

    [Header("Stats")]

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

    // If in Unity Editor K will kill the player
#if UNITY_EDITOR
    private void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.K) && !hasDied) 
        { 
            HandlePlayerDeath(); 
            Debug.Log(killCount);
        } 
    }
#endif


    private void Start()
    {
        playerHealth.OnDeath += HandlePlayerDeath;

        EnemyBase.OnAnyEnemyKilled += UpdateKillCount;



        canvasGroup = deathScreen.GetComponent<CanvasGroup>();
        deathScreen.SetActive(false);

        StartCoroutine(TimeSurvived()); // Begin timer
    }

    private void HandlePlayerDeath()
    {
        // Make sur ethe canvas's alpha is 0 and enablt the death screen
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
       // roundNum = waveManager.currentWaveIndex + 1; // +1 so round 1 != 0
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

    // Prevent memory leaks from enemy kill tracking
    private void OnDestroy()
    {
        playerHealth.OnDeath -= HandlePlayerDeath;

        EnemyBase.OnAnyEnemyKilled -= UpdateKillCount;
    }


}
