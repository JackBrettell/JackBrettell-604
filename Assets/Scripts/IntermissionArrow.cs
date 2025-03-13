using UnityEngine;

public class IntermissionArrow : MonoBehaviour
{
    public Transform store;
    public GameObject arrow;

    private void Update()
    {
        UpdateArrow();
    }

    private void UpdateArrow()
    {
        gameObject.transform.LookAt(store);
    }
}
