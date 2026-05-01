using UnityEngine;

public class ControlV2 : MonoBehaviour
{
    [Header("Références")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Paramètres")]
    public float speed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;

    [Header("Facing & Raycast Front")]
    [SerializeField] private Transform frontRayOrigin;
    [SerializeField] private float frontRayLength = 1.5f;

    // Flags (comme ton code original)
    private bool gauche;
    private bool droite;
    private bool gaucheRun;
    private bool droiteRun;
    private bool saut;

    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (frontRayOrigin == null)
        {
            GameObject frontObj = new GameObject("FrontRay");
            frontObj.transform.SetParent(transform);
            frontObj.transform.localPosition = new Vector3(0.8f, 0f, 0f);
            frontRayOrigin = frontObj.transform;
            frontRayOrigin.gameObject.hideFlags = HideFlags.HideInHierarchy;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            gauche = true;
            if (Input.GetKey(KeyCode.LeftShift)) gaucheRun = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            droite = true;
            if (Input.GetKey(KeyCode.LeftShift)) droiteRun = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            saut = true;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;

        // === MOUVEMENT ===
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

        // === SAUT ===
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

        // === ROTATION DU JOUEUR + TOUS LES ENFANTS ===
        HandleFacing(velocity.x);
    }

    private void HandleFacing(float currentVelocityX)
    {
        bool shouldFaceRight = currentVelocityX > 0.1f;
        bool shouldFaceLeft = currentVelocityX < -0.1f;

        if (shouldFaceRight && !isFacingRight)
        {
            isFacingRight = true;
            RotatePlayer();
        }
        else if (shouldFaceLeft && isFacingRight)
        {
            isFacingRight = false;
            RotatePlayer();
        }
    }

    private void RotatePlayer()
    {
        // Rotation 180° sur Y → tous les enfants (zone d’attaque, etc.) tournent avec
        float targetY = isFacingRight ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, targetY, 0f);
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

    // === GIZMO RAYCAST TOUJOURS VISIBLE DANS L'ÉDITEUR ===
    private void OnDrawGizmos()
    {
        if (frontRayOrigin == null) return;

        Gizmos.color = Color.red;
        Vector3 direction = isFacingRight ? Vector3.right : Vector3.left;
        Gizmos.DrawRay(frontRayOrigin.position, direction * frontRayLength);

        // Petite flèche pour mieux voir la direction
        Vector3 arrowPos = frontRayOrigin.position + direction * frontRayLength;
        Gizmos.DrawLine(arrowPos, arrowPos + new Vector3(-direction.x * 0.2f, 0.2f, 0));
        Gizmos.DrawLine(arrowPos, arrowPos + new Vector3(-direction.x * 0.2f, -0.2f, 0));
    }
}