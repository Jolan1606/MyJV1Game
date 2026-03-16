using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class CharacterAttack : MonoBehaviour
{
    private Animator anim;
    private GameManager manager;
    [SerializeField] private PlayerInput inputs;
    private InputAction attackAction;
    private CharaterMoves animator;
    private int director;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameManager.GetInstance();
        inputs = manager.GetInputs();
        anim = GetComponent<Animator>();
        attackAction = inputs.actions.FindAction("Attack"); // ligne super importante 
        animator = GetComponent<CharaterMoves>();
       // director = animator.direction;
    }

    // Update is called once per frame
    void Update()
    {
        float attack = attackAction.ReadValue<float>();
        
        if (attack == 1 )

        {
            anim.SetBool("attaque", true);



        }
        else if (attack == 0)
        {
            anim.SetBool("attaque", false);

        }

    }
}
