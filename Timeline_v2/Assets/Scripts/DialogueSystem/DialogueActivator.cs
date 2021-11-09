using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement PlayerMovement))
        {
            PlayerMovement.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement PlayerMovement))
        {
            if(PlayerMovement.Interactable is DialogueActivator dialogueActivator && (bool)dialogueActivator ==this)
            {
                PlayerMovement.Interactable = null;
            }
        }
    }
    public void Interact(PlayerMovement PlayerMovement)
    {
        if (TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject==dialogueObject)
        {
            PlayerMovement.DialogueUI.AddResponseEvents(responseEvents.Events);
        }
        PlayerMovement.DialogueUI.ShowDialogue(dialogueObject);
    }
}
