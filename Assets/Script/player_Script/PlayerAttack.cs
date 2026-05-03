using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private SandHealth currentTarget;   

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attaque déclenchée !");

            if (currentTarget != null)
            {
                Debug.Log("Attaque réussie sur l'ennemi !");
                currentTarget.TakeDamage(20f);
            }
            else
            {
                Debug.Log("Aucun ennemi à portée...");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            
            currentTarget = collision.GetComponent<SandHealth>();

            if (currentTarget != null)
            {
                Debug.Log("Ennemi détecté et cible verrouillée !");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            currentTarget = null;
        }
    }
}
