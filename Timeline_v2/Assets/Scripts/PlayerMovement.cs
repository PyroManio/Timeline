using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //NEW
    [SerializeField] private DialogueUI dialogueUI;

    public float moveSpeed = 5f;

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
        // Input
        movement.x = (int)(Input.GetAxisRaw("Horizontal")/0.0625)*0.0625f;
        movement.y = (int)(Input.GetAxisRaw("Vertical")/0.0625)*0.0625f;

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
