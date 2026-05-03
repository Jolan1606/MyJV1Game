using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public SandHealth SandHealth; // Référence à la classe de santé de l'ennemi
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       if (SandHealth == null)
        {
            SandHealth = Object.FindAnyObjectByType<SandHealth>();
            if (SandHealth == null)
                Debug.LogError("SandHealth introuvable ! Vérifie que ton Ennemy est bien dans la scène.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attaque déclenchée !");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.CompareTag("Ennemy"))
        {
            Debug.Log("Ennemi détecté !");
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
              
                
                    Debug.Log("Attaque réussie !");
                    SandHealth.TakeDamage(100f );

             
            }
        }
      
    }



}
