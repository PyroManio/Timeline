using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //NEW
    [SerializeField] private DialogueUI dialogueUI;

    public float moveSpeed = 5f;
    
    // this variable might end up never being used, but it might be helpful
    public string direction = "Down";
    //NEW
    public DialogueUI DialogueUI => dialogueUI;
    //NEW
    public IInteractable Interactable { get; set; }

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.IsOpen) 
        {
            movement.x=0;
            movement.y=0;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            return;
        }
        // Input
        movement.x = (int)(Input.GetAxisRaw("Horizontal")/0.0625)*0.0625f;
        movement.y = (int)(Input.GetAxisRaw("Vertical")/0.0625)*0.0625f;
        // if player is moving
        if (!(movement.x == 0 && movement.y == 0))
        {
            //animator will remeber that last direction the player was moving before stopping
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
            //if player has more horizontal movement
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y) )
            {
                if (movement.x > 0) direction = "Right";
                else direction = "Left";
            }
            // if player has more vertical movement
            else
            {
                if (movement.y > 0) direction = "Up";
                else direction = "Down";
            }
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate() 
    {
        // Movement
        Vector2 temp;
        temp=rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        temp.x= Mathf.RoundToInt( temp.x / 0.0625f) * 0.0625f;
        temp.y= Mathf.RoundToInt( temp.y / 0.0625f) * 0.0625f;
        
        rb.MovePosition( temp);
        //NEW
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(Interactable != null)
            {
                Interactable.Interact(PlayerMovement:this);
            }
        }
    }
}
