using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpProgress : MonoBehaviour
{
    //Ight, this stuff is gonna make sure that the help dialogue that is shown is accurate to the progress of the player
    [SerializeField] private DialogueActivator helpActivator;
    [SerializeField] private DialogueObject[] helpDialogueList;
    //[SerializeField] private DialogueUI dialogueUI;
    public int helpProgressFlag;
    public void playHelp(DialogueUI dialogueUI)
    {
        helpActivator.Interact(dialogueUI);
    }
}
 