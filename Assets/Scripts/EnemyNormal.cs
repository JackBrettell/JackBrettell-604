using UnityEngine;
using UnityEngine.UI;


public class EnemyNormal : Enemy, IDamageable
{
    private Slider healthSlider;
    private GameObject healthSliderObj;

    private GameObject player;


    private void Start()
    {

        player = GameObject.Find("Player");
        healthSliderObj = GameObject.Find("HealthBar");
        healthSlider = healthSliderObj.GetComponent<Slider>();

        // Initialize values
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void Update()
    {
        healthSlider.value = health;
        healthSliderObj.transform.LookAt(player.transform.position);
        healthSliderObj.transform.Rotate(0, 180, 0);

    }

    public override void TakeDamage(int damage)
    {
  
        health -= damage;
        Debug.Log($"{damage} damage received. Health: {health}");

        if (health < 0)
        {
            Destroy(gameObject);

        };
    }
}
