using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public bool pending;
    [SerializeField]
   private HealthSlider healthSlider;
    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        pending = true;
        if (healthSlider == null)
        {
            healthSlider = Object.FindAnyObjectByType<HealthSlider>();

            if (healthSlider == null)
                Debug.LogError("HealthSlider introuvable ! Vérifie que ton Player est bien dans la scène.");
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }





    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("zone d'attaque activée");
            if (pending == true)
            {

                StartCoroutine(frappe());
            }
        }
    }

    


    private IEnumerator frappe()
    {

        pending = false;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(1);
        
        Debug.Log("tu prend des degats");
       healthSlider.TakeDamage(5);
       yield return new WaitForSeconds(1);
        pending = true;

    }
        



   
}
