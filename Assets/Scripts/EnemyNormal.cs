using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyNormal : EnemyBase, IDamageable
{




    protected override void Awake()
    {
        health = 100;
        damage = 100;

        base.Awake();

        mainCollider = GetComponent<Collider>(); // Store main collider
        mainCollider.enabled = true;
    }

   
    public virtual void ToggleKinematics()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !rb.isKinematic;

            Debug.Log("Kinematics toggled");
        }
    }
}

