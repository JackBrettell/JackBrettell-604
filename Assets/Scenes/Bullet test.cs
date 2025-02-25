using UnityEngine;

public class Bullettest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        Bodytest body = other.GetComponentInParent<Bodytest>();

        body.PartHit(other.gameObject);

    }
}
