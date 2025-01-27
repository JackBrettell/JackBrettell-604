using UnityEngine;


// IDamageable interface
public interface IDamageable
{
    void TakeDamage(int damage); 
}

public class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public int damage;
    public float attackSpeed;
    public float movementSpeed;

    // Recieve damage
    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EnemyDeath(); 
        }
    }

    // Give damage
    public virtual void DoDamage()
    {
        Debug.Log($"Enemy dealt {damage} damage!");
    }

    public virtual void EnemyDeath()
    {
        Debug.Log("Enemy killed");
       // Destroy(gameObject); // Placeholder
    }
}

