using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Vitesse")]
    public float vitessePatrouille = 3f;      // vitesse quand il se balade
    public float vitesseCourse = 4.5f;        // un peu plus rapide quand il voit le joueur

    [Header("Attaque")]
    public float distancePourAttaquer = 1.2f; // à quelle distance il se colle et tape
    public float tempsEntreAttaques = 1f;

    // ======================== VARIABLES ========================
    private Rigidbody2D rb;
    private Transform joueur;
    private SpriteRenderer[] tousLesSprites;

    private bool joueurDansZone = false;       // ← c'est ÇA qui remplace le raycast
    private bool vaADroite = true;
    private float dernierCoup = 0f;
    private float dernierChangementDirection = 0f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joueur = GameObject.FindGameObjectWithTag("Player").transform;

        tousLesSprites = GetComponentsInChildren<SpriteRenderer>();

        Debug.Log("✅ Ennemi prêt avec zone de détection Trigger !");
       
    }

    void Update()
    {
        if (joueurDansZone)
        {
            Debug.Log("👀 Joueur dans la zone → Mode chasse !");
            ChasserEtSeColler();
        }
        else
        {
            Patrouiller();
        }
    }

    // ======================== DÉTECTION TRIGGER ========================
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = true;
            Debug.Log("✅ Joueur ENTRE dans la zone de détection !");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = false;
            Debug.Log("❌ Joueur SORT de la zone");
        }
    }

    // ======================== PATROUILLE (adaptée désert) ========================
    void Patrouiller()
    {
        float dir = vaADroite ? 1 : -1;
        rb.linearVelocity = new Vector2(dir * vitessePatrouille, rb.linearVelocity.y);

        RetournerSprites(vaADroite);

        // Détection sol devant (tolérant aux bosses et creux)
        Vector2 devant = transform.position + (vaADroite ? Vector3.right : Vector3.left) * 0.8f;
        RaycastHit2D sol = Physics2D.Raycast(devant, Vector2.down, 2.2f);

        bool yADuSol = sol.collider != null && sol.collider.CompareTag("Sol");

        bool murDevant = Physics2D.Raycast(transform.position, vaADroite ? Vector2.right : Vector2.left, 0.8f).collider != null;

        if ((Time.time - dernierChangementDirection > 0.8f) && (!yADuSol || murDevant))
        {
            vaADroite = !vaADroite;
            dernierChangementDirection = Time.time;
            Debug.Log("🔄 Je tourne (à cause du désert)");
        }
    }

    // ======================== CHASSE + SE COLLER ========================
    void ChasserEtSeColler()
    {
        float distance = Vector2.Distance(transform.position, joueur.position);

        // Si assez proche → on s'arrête et on tape
        if (distance <= distancePourAttaquer)
        {
            rb.linearVelocity = Vector2.zero;   // on se colle complètement
           
            return;
        }

        // Sinon on fonce vers le joueur
        float dir = (joueur.position.x > transform.position.x) ? 1 : -1;
        rb.linearVelocity = new Vector2(dir * vitesseCourse, rb.linearVelocity.y);

        RetournerSprites(dir > 0);
    }

   
    void RetournerSprites(bool versDroite)
    {
        foreach (SpriteRenderer s in tousLesSprites)
        {
            s.flipX = !versDroite;
        }
    }

    // Pour voir la zone dans l'éditeur
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 8f); // même taille que ton trigger
    }
}