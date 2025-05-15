using UnityEngine;
using System.Collections;
public class WaveUIHandler : MonoBehaviour
{
    [SerializeField] private HUD hud;

    public void PrepareWave(WaveDefinition wave)
    {
        // Optional: Animate unlocks or other pre-wave UI
    }

   /* public IEnumerator RunIntermission(int seconds)
    {
        intermissionArrow.ToggleArrow();
        yield return new WaitForSeconds(seconds);
        intermissionArrow.ToggleArrow();
    }*/
}
