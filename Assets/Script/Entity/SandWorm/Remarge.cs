using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class Remarge : MonoBehaviour
{
    public GameObject SD;
    private bool alreadyTrigger; 
    public float emergeDuration = 1.0f; // Durée de l’émergence en secondes 
   [SerializeField] private SandHealth sHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

      
        //recupere la vie pour le coulage
        if (collision.gameObject.CompareTag("Ennemy") == true)
        {
            sHealth = collision.GetComponent<SandHealth>();
        }
    }
    
   

    private void FixedUpdate()
    {
        if(sHealth.health < 40 )
        {
StartCoroutine(SmoothRemage());
        }
    }

    IEnumerator SmoothRemage()
    {

        if (SD == null) yield break;

        // Position de départ 
        Vector3 startPos = SD.transform.position;

        // Position d'arrivée
        Vector3 targetPos = new Vector3(
            transform.position.x,
            transform.position.y - 1.5f,
            transform.position.z
        );
        
        float elapsed = 0f;

        while (elapsed < emergeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / emergeDuration;

            // Interpolation linéaire 
            SD.transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;   //  attend la prochaine frame
        }

        //  s’assure qu’il arrive pile à la fin
        SD.transform.position = targetPos;
    }
}
