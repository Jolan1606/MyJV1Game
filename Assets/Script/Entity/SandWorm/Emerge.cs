using UnityEngine;
using System.Collections;

public class Emerge : MonoBehaviour
{
    public GameObject SD;
    private bool alreadyTrigger; 
    public float emergeDuration = 1.0f; // Durée de l’émergence en secondes 

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") == true)
        {

            if (alreadyTrigger == true)
            {
                return;
            }
            else
            {
                Debug.Log("cadetecte");
                StartCoroutine(SmoothEmerge());
                //SD.transform.position = transform.position = new Vector3(transform.position.x, transform.position.y + (float)1.5, transform.position.z);
                alreadyTrigger = true;
            }
        }
    }
    private IEnumerator SmoothEmerge()
    {
        if (SD == null) yield break;

        // Position de départ 
        Vector3 startPos = SD.transform.position;

        // Position d'arrivée
        Vector3 targetPos = new Vector3(
            transform.position.x,
            transform.position.y + 1.5f,
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
