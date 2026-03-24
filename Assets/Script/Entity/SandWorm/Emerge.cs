using UnityEngine;

public class Emerge : MonoBehaviour
{
    public GameObject SD;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("cadetecte");

            SD.transform.position = transform.position = new Vector3(transform.position.x, transform.position.y + (float)1.5 , transform.position.z);

        }
    }
}
