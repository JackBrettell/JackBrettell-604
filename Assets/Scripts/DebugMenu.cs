using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void OnResetPlayerPosition()
    {
   
        player.transform.position = new Vector3(-153, 6, -7);
        player.transform.rotation = Quaternion.identity;
    
    }
}
