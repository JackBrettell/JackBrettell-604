using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Endingscene : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera cinematicCamera;
    [SerializeField] private SplineAnimate carSpline;
    [SerializeField] private float cinematicDuration = 5f;
    private void BeginCinematic()
    {
        // Disable the player object
        player.SetActive(false); 

        //Switch the active camera from player to cinematic
        playerCamera.enabled = false;
        cinematicCamera.gameObject.SetActive(true);
        cinematicCamera.enabled = true;

        // begin the car spline animation
        carSpline.Play();

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
        if (Input.GetKeyDown(KeyCode.L))
        {
            BeginCinematic();
        }
    }
}
