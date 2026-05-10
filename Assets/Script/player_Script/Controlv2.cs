using UnityEngine;


public class ControlV2 : MonoBehaviour
{
    [Header("Références")]
    private Rigidbody2D rb;
    private Transform graphics;
    [SerializeField] private Animator animator;
    private bool isWalking => (gauche || droite) ;
    [Header("Paramètres")]
    public float speed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 12f;
    public bool isrunning => (gaucheRun || droiteRun);

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;

    [Header("Facing & Raycast Front")]
    [SerializeField] private Transform frontRayOrigin;
    [SerializeField] private float frontRayLength = 1.5f;

    // Flags
    private bool gauche;
    private bool droite;
    private bool gaucheRun;
    private bool droiteRun;
    private bool saut;
   

    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        graphics = transform.Find("Graphics");

        if (graphics == null)
        {
            
            return;
        }

        if (frontRayOrigin == null)
        {
            GameObject frontObj = new GameObject("FrontRay");
            frontObj.transform.SetParent(transform);
            frontObj.transform.localPosition = new Vector3(0.8f, 0f, 0f);
            frontRayOrigin = frontObj.transform;
            frontRayOrigin.gameObject.hideFlags = HideFlags.HideInHierarchy;
        }
       
    }


    public void Update()
    {
        // Inputs 
        gauche = Input.GetKey(KeyCode.Q);
        droite = Input.GetKey(KeyCode.D);
        gaucheRun = gauche && Input.GetKey(KeyCode.LeftShift);
        droiteRun = droite && Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space))
            saut = true;

        // = ANIMATION 
        float currentSpeed = 0f;
        if (gauche || droite)
        {
            currentSpeed = (gaucheRun || droiteRun) ? runSpeed : speed;
        }
        else
        {
            currentSpeed = 0f;
        }
        animator.SetFloat("Speed", currentSpeed);
       
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;

        // Mouvement
        if (gauche)
        {
            velocity.x = gaucheRun ? -runSpeed : -speed;
            gauche = false;
            gaucheRun = false;
        }
        else if (droite)
        {
            velocity.x = droiteRun ? runSpeed : speed;
            droite = false;
            droiteRun = false;
        }
        else
        {
            velocity.x = 0f;
        }

        // Saut
        if (saut && isGrounded)
        {
            velocity.y = jumpForce;
            isGrounded = false;
            saut = false;
        }
        else
        {
            saut = false;
        }

        rb.linearVelocity = velocity;

        // Rotation du visuel + zone d'attaque fonctionnelle
        HandleFacing(velocity.x);
    }

    private void HandleFacing(float currentVelocityX)
    {
        bool shouldFaceRight = currentVelocityX > 0.1f;
        bool shouldFaceLeft = currentVelocityX < -0.1f;

        if (shouldFaceRight && !isFacingRight)
        {
            isFacingRight = true;
            RotateGraphics();
        }
        else if (shouldFaceLeft && isFacingRight)
        {
            isFacingRight = false;
            RotateGraphics();
        }
    }

    private void RotateGraphics()
    {
        // 180° sur Y → le sprite ET la zone d'attaque tournent ensemble
        float targetY = isFacingRight ? 0f : 180f;
        graphics.rotation = Quaternion.Euler(0f, targetY, 0f);
    }

    // Ground Check
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sol"))
            isGrounded = false;
    }

    private void OnDrawGizmos()
    {
        if (frontRayOrigin == null) return;
        Gizmos.color = Color.red;
        Vector3 direction = isFacingRight ? Vector3.right : Vector3.left;
        Gizmos.DrawRay(frontRayOrigin.position, direction * frontRayLength);
    }
}