using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider slider;

    private void Start()
    {

        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            TakeDamage(5);
        }
    }
    void TakeDamage(float damage)
    {
        health -= damage;
        slider.value = health;
        Invoke("Die", 1);
    }
    void Die()
    {
        if (health <= 0)
        {

            Debug.Log("t'es mort conanard ");

            health = maxHealth;
            slider.value = maxHealth;


        }




    }
}
