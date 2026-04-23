using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
<<<<<<< Updated upstream
    public bool pending; 
    public HealthSlider healthSlider;
=======
public HealthSlider playerHealth;
>>>>>>> Stashed changes
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
        pending = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



<<<<<<< Updated upstream

    private void OnTriggerStay2D(Collider2D collision)
=======
     IEnumerator SandAttack()
>>>>>>> Stashed changes
    {

<<<<<<< Updated upstream
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("zone d'attaque activée");
            if (pending == true)
            {
                
                StartCoroutine(frappe());
            }
=======


    yield return null;
}
     void OnTrigger2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
         StartCoroutine (SandAttack());

>>>>>>> Stashed changes
        }
    }
    private IEnumerator frappe()
    {
        pending = false;
        yield return new WaitForSeconds(1);
        Debug.Log("tu prend des degats");
        healthSlider.TakeDamage(5);
       yield return new WaitForSeconds(1);
        pending = true;
    }
        



   
}
