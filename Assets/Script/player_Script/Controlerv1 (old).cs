using Unity.VisualScripting;
using UnityEngine;

public class CapsuleSaut : MonoBehaviour
{
    //mouvement
    private Rigidbody2D rb;
    private float speed = 10f;
    private Vector3 objectVelocity;
    private float speedForce = 10f;
    private float speedSpeed;
    //jump 
    [SerializeField]
    private float jumpForce;
    private float jumpSpeed;
    private bool isjumping;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PERMET DE ALLER chercher un compenant dans l'objet ( scirpt ou composant unity ) 
        rb = GetComponent<Rigidbody2D>();
        // va chercher la valeur de gravité dans  le moteur 
        objectVelocity.y = Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSide();
        CheckInputBuffer();
        JumpComp();

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = objectVelocity;




    }
    public void MoveSide()
    {
        if (Input.GetKey(KeyCode.Q) && isGrounded == true)
        {
            speedSpeed = speedForce;

            objectVelocity.z = 1 * speedSpeed;


        }
        else if (Input.GetKey(KeyCode.D) && isGrounded == true)
        {
            speedSpeed = speedForce;

            objectVelocity.z = -1 * speedSpeed;

        }
        else
        {

            objectVelocity.z = 0;
        }




    }
    public void JumpComp()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isjumping == false && isGrounded == true)
            {
                jumpSpeed = jumpForce;
                isjumping = true;
            }
            if (isGrounded == false)
            {
                UseInputBuffer();

            }
        }
        jumpSpeed = jumpSpeed - 2 * Time.deltaTime;
        if (jumpSpeed <= 0)
        {
            jumpSpeed = 0;

        }
        objectVelocity.y = Physics.gravity.y - Physics.gravity.y * jumpSpeed;


    }
    //input buffer 
    private bool isInInputbuffer;
    [SerializeField]
    private float timeInputBuffer;
    private float chronoInputBuffer;

    public void CheckInputBuffer()
    {
        if (isInInputbuffer == true)
        {
            chronoInputBuffer = chronoInputBuffer - Time.deltaTime;
        }
        if (chronoInputBuffer <= 0)
        {

            chronoInputBuffer = 0;
            isInInputbuffer = false;

        }

    }
    public void UseInputBuffer()
    {
        isInInputbuffer = true;

        chronoInputBuffer = timeInputBuffer;

    }
    //fin

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "sol")
        {
            isGrounded = true;
            if (isInInputbuffer == false)
            {
                isjumping = false;


            }
            else
            {
                jumpSpeed = jumpForce;

            }
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == "sol")
        {
            isGrounded = false;
        }


    }
}
