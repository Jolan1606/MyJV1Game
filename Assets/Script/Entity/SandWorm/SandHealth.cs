using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SandHealth : MonoBehaviour
{
    public float health = 100f;
    public Watersource waterDrop;
    public GameObject waterDropPrefab;
    public int dropChance; // Chance de faire tomber une goutte d'eau (en pourcentage)
    public GameObject SD;
    private bool alreadyTrigger;
    public float emergeDuration = 1.0f;
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

        if (Random.Range(0, 100) < dropChance)
        {
            Instantiate(waterDropPrefab, this.transform.position, Quaternion.identity);
            waterDrop.gameObject.SetActive(true);
        }

    }
    private void FixedUpdate()
    {
        if (health < 40)
        {
            StartCoroutine(SmoothDrown());
        }
    }



    IEnumerator SmoothDrown()
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


