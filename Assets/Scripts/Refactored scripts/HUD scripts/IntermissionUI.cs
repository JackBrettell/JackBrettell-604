using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class IntermissionUI : MonoBehaviour
{

    [Header("Intermission")]
    [SerializeField] private GameObject intermissionUI;
    [SerializeField] private TMP_Text intermissionCountdown;
    [SerializeField] private Transform store;
    [SerializeField] GameObject arrowObj;

    public Action OnIntermissionComplete;

    private void Update()
    {
       UpdateArrow();
    }


    public void StartIntermissionTimer(WaveDefinition waveInfo)
    {
        int duration = waveInfo.intermissionDuration;
        StartCoroutine(IntermissionCountdown(duration));
    }

    private IEnumerator IntermissionCountdown(int duration)
    {
        arrowObj.SetActive(true);

        intermissionUI.SetActive(true);
    

        for (int timeLeft = duration; timeLeft >= 0; timeLeft--)
        {
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;

            intermissionCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
        }

        intermissionCountdown.text = "00:00";
        intermissionUI.SetActive(false);
        arrowObj.SetActive(false);

        OnIntermissionComplete?.Invoke();
    }

    public void UpdateArrow()
    {
        arrowObj.transform.LookAt(store);
    }
}
