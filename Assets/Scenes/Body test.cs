using UnityEngine;

public class Bodytest : MonoBehaviour
{
    public void PartHit(GameObject part)
    {
        print(part.name);

        if (part.name == "Head")
        {
            print("Headshot!");
        }
        else
        {
            print("Hit!");
        }
    }

}
