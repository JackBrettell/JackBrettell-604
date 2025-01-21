using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void Start()
    {
        int x = 5;
        int y = 5;
        int z = 5;

        DoSomething(x);
        DoSomethingRef(ref y);
        DoSomethingOut(out z);

        Debug.Log(" ");

        int.TryParse("2", out int num);
    }

    public void DoSomethingOut(out int Num1)
    {
        Num1 = 5;
        Num1++;
    }

    public void DoSomethingRef(ref int Num1)
    {
        Num1++;
    }

    public void DoSomething(int Num1)
    {
        Num1++;
    }
}
