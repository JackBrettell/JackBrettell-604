using UnityEngine;

public class IntermissionUI : MonoBehaviour
{
   [SerializeField] public Transform store;

    private void Update()
    {
        UpdateArrow();
    }

    public void ToggleArrow()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void UpdateArrow()
    {
        gameObject.transform.LookAt(store);
    }
}
