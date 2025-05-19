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

    private void Update()
    {
       UpdateArrow();
    }
    public void StartIntermissionTimer(float duration)
    {
        arrowObj.SetActive(true);
        intermissionUI.SetActive(true);
        intermissionCountdown.text = string.Format("{0:00}:{1:00}", duration / 60, duration % 60);
    }

    public void IntermissionCompleted()
    {
        intermissionUI.SetActive(false);
        arrowObj.SetActive(false);
    }

    public void UpdateArrow()
    {
        arrowObj.transform.LookAt(store);
    }
}
