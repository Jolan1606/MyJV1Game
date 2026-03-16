
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;  // Pour Tilemap[]

public class clickToMove2D : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float movesSpeed = 5f;
    private Vector2 targetPosition;
    private bool haveTarget = false;
    private Camera cam;
    private Animator anim;
    

    // GameManager & Inputs (inchangé)
    private GameManager manager;
    [SerializeField] private PlayerInput inputs;
    private InputAction targetAction;

    // Grid & Tilemaps marchables (cliquables)
    public Grid levelGrid;                    // Drag ton "Grid" ici
    public Tilemap[] clickableTilemaps;       // Drag "terrain" ET "decors sans collision" ici (tu peux en ajouter plusieurs)

    void Start()
    {
        manager = GameManager.GetInstance();
        rigid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        inputs = manager.GetInputs();
        targetAction = inputs.actions.FindAction("Target");

        // Auto-find Grid si oublié
        if (levelGrid == null) levelGrid = FindObjectOfType<Grid>();

        // Auto-find toutes les Tilemaps enfants du Grid (pratique !)
        if (clickableTilemaps == null || clickableTilemaps.Length == 0)
        {
            clickableTilemaps = levelGrid.GetComponentsInChildren<Tilemap>();
            Debug.Log("Auto-find : " + clickableTilemaps.Length + " Tilemaps trouvées");
        }
    }

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        float _Move = targetAction.ReadValue<float>();
        if (_Move == 1)
        {
            Vector2 screenPos = mouse.position.ReadValue();
            Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
            worldPos.z = 0f;

            Debug.Log("=== CLIC ===");
            Debug.Log("World pos : " + worldPos);

            // Meilleur calcul de la cellule (arrondi au plus proche, fixe les bords !)
            Vector3Int cellPos = levelGrid.WorldToCell(worldPos);
            // Correction manuelle pour arrondi parfait
            Vector3 cellCenter = levelGrid.GetCellCenterWorld(cellPos);
            Vector3 offset = worldPos - cellCenter;
            if (Mathf.Abs(offset.x) > 0.5f) cellPos.x += (int)Mathf.Sign(offset.x);
            if (Mathf.Abs(offset.y) > 0.5f) cellPos.y += (int)Mathf.Sign(offset.y);

            Debug.Log("Cell pos finale : " + cellPos);

            // Check si AU MOINS UNE Tilemap a une tile ici
            bool hasTile = false;
            string whichTilemap = "";
            foreach (Tilemap tm in clickableTilemaps)
            {
                if (tm.HasTile(cellPos))
                {
                    hasTile = true;
                    whichTilemap = tm.name;
                    break;
                }
            }

            if (hasTile)
            {
                targetPosition = levelGrid.GetCellCenterWorld(cellPos);
                haveTarget = true;
                Debug.Log("✓ DÉTECTÉ sur : " + whichTilemap + " → Cible : " + targetPosition);
            }
            else
            {
                Debug.Log("✗ Aucune tile sur aucune Tilemap à cette case");
            }
            Debug.Log("===========");
        }
        //animation 
        //anim.SetInteger("Direction", direction);



    }
    
    private void FixedUpdate()
    {
        if (haveTarget)
        {
            rigid.MovePosition(Vector2.MoveTowards(rigid.position, targetPosition, movesSpeed * Time.fixedDeltaTime));

            if (Vector2.Distance(rigid.position, targetPosition) < 0.1f)
            {
                haveTarget = false;
                Debug.Log("Arrivé !");
            }
        }
    }

    
}





