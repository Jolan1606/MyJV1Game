using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Vector3 originalScale;   // ← on garde la taille de départ pour éviter les déformations

    void Start()
    {
        // On prend une "photo" de la taille actuelle de l'enfant (important !)
        originalScale = transform.localScale;

        // Recherche automatique du joueur
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
            else
                Debug.LogError("❌ Player introuvable ! Vérifie que ton joueur a le tag 'Player'");
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        // 1. Calcul de la différence en X (comme on a vu ensemble)
        float diffX = playerTransform.position.x - transform.position.x;

        // 2. On décide la direction (+1 = droite, -1 = gauche)
        float direction = (diffX > 0) ? -1f : 1f;

        // 3. On applique le flip EN GARDANT la taille originale en Y et Z
        transform.localScale = new Vector3(
            originalScale.x * direction,   // on inverse seulement le X
            originalScale.y,               // Y reste exactement comme au début
            originalScale.z                // Z reste exactement comme au début
        );
    }
}