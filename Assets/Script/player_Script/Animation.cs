using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator animator;
     void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public bool isWalking => animator.GetBool("isWalking");
    public ControlV2 player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = Object.FindAnyObjectByType<ControlV2>();
            if (player == null)
                Debug.LogError("Player introuvable ! Vérifie que ton Player est bien dans la scène.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking == true)
        {
      animator.SetBool("isWalking", true);
        }
     /*   else if (isWalking == false)
        {
            animator.SetBool("isWalking", true);


        }*/
    }
}
