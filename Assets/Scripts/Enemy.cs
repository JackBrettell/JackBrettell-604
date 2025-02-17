using UnityEngine;
using UnityEngine.AI;


// IDamageable interface
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


    // Recieve damage
    public virtual void TakeDamage(int damage)
    {

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

