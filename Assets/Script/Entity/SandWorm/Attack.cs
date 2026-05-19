using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool pending;
    [SerializeField] private HealthSlider healthSlider;
    [SerializeField] private Animator animator;

    private bool playerInRange = false;           // ← nouveau
    private Coroutine attackRoutine = null;       // ← gère la boucle continue

    void Start()
    {
        pending = true;

        if (healthSlider == null)
        {
            healthSlider = Object.FindAnyObjectByType<HealthSlider>();
            if (healthSlider == null)
                Debug.LogError("HealthSlider introuvable !");
        }
    }

    // ====================== ENTRÉE / SORTIE ======================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("zone d'attaque activée - boucle lancée");

            if (attackRoutine == null)                    // on lance la boucle UNE SEULE fois
            {
                attackRoutine = StartCoroutine(AttackLoop());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("joueur sorti - boucle arrêtée");

            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                attackRoutine = null;
            }
        }
    }

    // ====================== BOUCLE D'ATTAQUE CONTINUE ======================
    private IEnumerator AttackLoop()
    {
        while (playerInRange)           // tant qu'il reste dedans → on recommence à l'infini
        {
            if (pending)
            {
                yield return StartCoroutine(frappe());   // on attend que l'attaque complète se termine
            }
            else
            {
                yield return null;   // petite sécurité si pending est bloqué
            }
        }

        attackRoutine = null;   // on nettoie une fois sorti
    }

    // ====================== UNE SEULE ATTAQUE ======================
    public IEnumerator frappe()
    {
        pending = false;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(1f);   // temps d'animation avant le coup

        // Sécurité : si le joueur est sorti entre-temps, on annule les dégâts
        if (playerInRange)
        {
            Debug.Log("tu prend des degats");
            healthSlider.TakeDamage(5);
        }

        yield return new WaitForSeconds(1f);   // cooldown entre deux attaques

        pending = true;
    }
}