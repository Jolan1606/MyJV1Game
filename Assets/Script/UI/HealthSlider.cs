
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider slider;
    public GameObject lastCheckpoint;
    public WaterSlider waterSlider;

    private void Start()
    {
        if (waterSlider == null)
        {
            waterSlider = Object.FindAnyObjectByType<WaterSlider>();
            if (waterSlider == null)
                Debug.LogError("WaterSlider introuvable ! Vérifie que ton Player est bien dans la scène.");
        }
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
    }
    private void Update()
    {
        Die();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        slider.value = health;
        Invoke("Die", 1);
    }
     public void Die()
    {
        if (health <= 0)
        {
            this.transform.position = lastCheckpoint.transform.position;

            health = maxHealth;
            slider.value = maxHealth;
            waterSlider.water = waterSlider.MaxWater;
            waterSlider.slider.value = waterSlider.MaxWater;

        }




    }

void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint reached");
            lastCheckpoint = collision.gameObject;
        }
    }


}
