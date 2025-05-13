using UnityEngine;

[CreateAssetMenu(fileName = "WeaponList", menuName = "Scriptable Objects/WeaponList")]
public class WeaponList : ScriptableObject
{
    [SerializeField] private WeaponStats[] weaponStatsList;
    public WeaponStats[] WeaponStatsList => weaponStatsList;

    [SerializeField] private GunBehaviour[] gunBehaviourList;
    public GunBehaviour[] GunBehaviourList => gunBehaviourList;
}
