using UnityEngine;
using UnityEngine.UI;

public class SandHealth : MonoBehaviour
{
   public float health = 100f;
    public Watersource waterDrop;
    public GameObject waterDropPrefab;
    public int dropChance ; // Chance de faire tomber une goutte d'eau (en pourcentage)
    void Start()
    {
       
        if (waterDropPrefab == null)
        {
            waterDropPrefab = Object.FindAnyObjectByType<Watersource>().gameObject;
            if (waterDropPrefab == null)
                Debug.LogError("WaterDropPrefab introuvable ! Vérifie que ton WaterDropPrefab est bien dans la scène.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            
        }
        Debug.Log("SandWorm Health: " + health);
    }

    private void Die()
    {
        // Logique de mort du ver de sable
        Destroy(gameObject);

        if(Random.Range(0, 100) < dropChance)
        {
            Instantiate(waterDropPrefab, this.transform.position, Quaternion.identity);
            waterDrop.gameObject.SetActive(true); 
        }
        
    }
}