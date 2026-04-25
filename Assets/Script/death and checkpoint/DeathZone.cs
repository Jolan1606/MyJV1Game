using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] 
    private HealthSlider healthSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (healthSlider == null)
        {
            healthSlider = Object.FindAnyObjectByType<HealthSlider>();

            if (healthSlider == null)
                Debug.LogError("HealthSlider introuvable ! Vérifie que ton Player est bien dans la scène.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        



    }



void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("zone de mort activée");
            healthSlider.TakeDamage(healthSlider.maxHealth);
        }
    }
}
