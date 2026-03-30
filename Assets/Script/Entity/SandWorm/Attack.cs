using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attackCible;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    /*private IEnumerator Attack()
    {
      




    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           


        }
    }


}
