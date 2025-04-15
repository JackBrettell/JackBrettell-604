using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Endingscene : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera cinematicCamera;
    [SerializeField] private SplineAnimate carSpline;
    [SerializeField] private float cinematicDuration = 5f;

    [Header("Zombies")]
    [SerializeField] private GameObject[] zombies;
    [SerializeField] private float zombieSpeed = 1f;
    [SerializeField] private GameObject zombieTarget;

    [Header("Car interact")]
    [SerializeField] private GameObject carInteract;
    [SerializeField] private float playerInteractRange = 0;

    private void BeginCinematic()
    {
        // Disable the player/UI objects
        player.SetActive(false); 
        carInteract.SetActive(false);

        //Switch the active camera from player to cinematic
        playerCamera.enabled = false;
        cinematicCamera.gameObject.SetActive(true);
        cinematicCamera.enabled = true;

        // begin the car spline animation
        carSpline.Play();

        // Enable and move zombies forward
        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(true);
            zombie.transform.DOMove(zombieTarget.transform.position, zombieSpeed).SetEase(Ease.Linear);
        }



        StartCoroutine(EndCinematic());

    }

    private IEnumerator EndCinematic()
    {
        // Wait for the car spline animation to finish
        yield return new WaitForSeconds(cinematicDuration);


        // Disable the player object
        player.SetActive(true);

        //Switch the active camera from player to cinematic
        playerCamera.enabled = true;
        cinematicCamera.gameObject.SetActive(false);
        cinematicCamera.enabled = false;
    }

        // Update is called once per frame
    void Update()
    {
        CarInteraction();


        if (Input.GetKeyDown(KeyCode.L))
        {
            BeginCinematic();
        }
    }

    private void CarInteraction()
    {
        float distanceToCar = Vector3.Distance(player.transform.position, carInteract.transform.position);

        if (distanceToCar < playerInteractRange)
        {
            carInteract.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                BeginCinematic();
            }
        }
        else
        {
            carInteract.SetActive(false);
        }
    }
}
