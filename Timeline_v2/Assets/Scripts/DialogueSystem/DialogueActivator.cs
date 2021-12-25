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
                if (PlayerMovement.Interactable.Equals(this))
                    PlayerMovement.Interactable = null;
            }
        }
    }
    public void Interact(PlayerMovement PlayerMovement)
    {
       Interact(PlayerMovement.DialogueUI);
    }
    public void Interact(DialogueUI dialogueUI)
    {
        ResponseEvent[][] compResponseEvents = new ResponseEvent[GetComponents<DialogueResponseEvents>().Length][];
        //List<ResponseEvent[]>
        int tempIndex=0;
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            compResponseEvents[tempIndex] = responseEvents.Events;
            /*if (responseEvents.DialogueObject == dialogueObject)
            {
                dialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }*/
            tempIndex++;
        }
        dialogueUI.AddResponseEvents(compResponseEvents);
        dialogueUI.ShowDialogue(dialogueObject);
    }
}
