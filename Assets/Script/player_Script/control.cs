using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;


public class control : MonoBehaviour
{
    Rigidbody2D rb;
    private bool saut;
    private bool droite;
    private bool gauche;
    private bool gaucheRun;
    private bool droiteRun;
    public float speed = 5f ;
    private Vector2 rbSpeed;
    private Vector2 objectVelocity;
    private float jumpSpeed;
    public float jumpForce;
    private bool isjumping;
   [SerializeField] private bool isGrounded;
    private void Awake()
    {
        jumpForce = 5f ;
        rb = GetComponent<Rigidbody2D>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectVelocity.y = Physics.gravity.y;
        rbSpeed = rb.linearVelocity;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {

          
            gauche = true;
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("ici c'est detecté");
                gaucheRun = true;

            }

        }
        else if (Input.GetKey(KeyCode.D))
        {


            

            droite = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("ici c'est detecté");
                droiteRun = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            saut = true;

        }

        Debug.Log(isGrounded);

    }
    private void FixedUpdate()
       
    {
        Debug.Log(isGrounded);
        if (isGrounded == false) 
        {
         objectVelocity.y += Physics.gravity.y * Time.fixedDeltaTime;
        }
        rb.linearVelocity = objectVelocity;
        if (gauche == true)
        {

            
            objectVelocity.x = -1 * speed;
            
            gauche = false;
        }
        else if (droite == true)
        {


           
            objectVelocity.x = 1 * speed;
           
            droite = false;


        }
        else
        {
            objectVelocity.x = 0;


        }

        if (droiteRun == true) //fonction courir 
        {
            Debug.Log("il cours");
            objectVelocity.x = 1 * 10f;
            droiteRun = false;


        }
        else if (gaucheRun == true) //fonction courir 
        {
            objectVelocity.x = -1 * 10f;
            gaucheRun = false;
            Debug.Log("il cours");
        }





        if (saut == true)  //fonction saut 
        {
            if (isGrounded == true)
            {
                objectVelocity.y = jumpForce;
                isGrounded = false;
            }
           
        }
        saut = false;
    }
     void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "sol")
        {
            isGrounded = true;
          
        }
    }
     void OnCollisionExit(Collision collision)
    {
        Debug.Log("sortieA16");
        if (collision.transform.name == "sol")
        {
            isGrounded = false;
        }


    }
    

}
