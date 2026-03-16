using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class CharaterMoves : MonoBehaviour
{

    [SerializeField] private PlayerInput inputs;
    private GameManager manager;
    private InputAction movesAction;
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;
    private Animator anim;
    private int direction =0;
    [SerializeField] private float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GameManager.GetInstance();
        inputs = manager.GetInputs();
        movesAction = inputs.actions.FindAction("Move");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
      
         Vector2 _MoveValue = movesAction.ReadValue<Vector2>(); // recupere la direction des touche du clavier via le input syteme dans un vector 2 et le stock 
         _MoveValue = ChosseDirection(_MoveValue);
         

         rb.MovePosition((Vector2)rb.position + _MoveValue * speed * Time.fixedDeltaTime);// aplique les valeur de deplacement du _MoveValue avec un rigidBody2D


        //animation 
        anim.SetInteger("Direction", direction);






    }
    private Vector2 ChosseDirection(Vector2 _Value) //empeche le mouvement en diagonale 
    {
        Vector2 _Result = Vector2.zero;
        if (Mathf.Abs(_Value.x) > Mathf.Abs(_Value.y))// se deplace en x 
        {
           
         _Result = new Vector2(_Value.x, 0);


        }
        else
        {

            _Result = new Vector2(0, _Value.y);


        }
     
      direction = SetDirection(_Result); //prends resulte et le transforme en _vector pour comparasion plus bas et stoque totu ça dans direction 
        return _Result;
        
        

        


    }

    public int SetDirection(Vector2 _Vector)
    {
        if (_Vector.x > 0)
        {
            return 6;

        }
        if (_Vector.x < 0)
        {
            return 4;

        }
        if(_Vector.y > 0) 
        {
            return 8;
        }
        if (_Vector.y < 0)
        {
            return 2;
        }
        return 0;
    }
}
