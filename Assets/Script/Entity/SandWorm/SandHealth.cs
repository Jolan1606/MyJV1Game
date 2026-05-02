using UnityEngine;

public class SandHealth : MonoBehaviour
{
   private float health = 100f;
    void Start()
    {
        
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
    }
}