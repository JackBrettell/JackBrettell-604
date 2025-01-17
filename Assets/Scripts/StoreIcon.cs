using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject player;
    public GameObject storeIcon;
    public float triggerRange = 1;
    public float rotationSpeed = 2f; // Speed of the smooth rotation

    // Update is called once per frame
    void Update()
    {
    
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.y = 0; 


        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance <= triggerRange)
        {
            storeIcon.SetActive(true);
        }
        else
        {
            storeIcon.SetActive(false);
        }


        Debug.Log(playerDistance);
    }
}
