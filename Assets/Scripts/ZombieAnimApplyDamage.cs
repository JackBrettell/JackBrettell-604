using UnityEngine;

public class ZombieAnimApplyDamage : MonoBehaviour
{
    private EnemyNormal enemyNormal;

    private void Start()
    {
        enemyNormal = GetComponentInParent<EnemyNormal>();
    }

    public void ApplyDamage()
    {
        enemyNormal.ApplyDamage();
    }
}
