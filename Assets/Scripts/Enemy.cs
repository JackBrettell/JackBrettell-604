using UnityEngine;
using System.Collections;
public interface IDamageable
{
    void TakeDamage(int damage);
}
public class Enemy : MonoBehaviour, IDamageable
{
    protected int health;
    protected int damage;
    protected float attackSpeed;
    protected float movementSpeed;

    private EnemyFactory factory;
    public EnemyType enemyType;

    // Death event
    public delegate void DeathHandler();
    public event DeathHandler OnDeath;
    public float timeTilDespawn = 3f;

    private void Awake()
    {
        factory = FindFirstObjectByType<EnemyFactory>();
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{damage} damage received. Health: {health}");

        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    public virtual void DoDamage()
    {
        Debug.Log($"Enemy dealt {damage} damage!");
    }

    public virtual void EnemyDeath()
    {
        Debug.Log("Enemy killed");

        
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeTilDespawn);

        OnDeath.Invoke();
        gameObject.SetActive(false);

        factory.ReturnEnemy(enemyType, gameObject);

    }
}
