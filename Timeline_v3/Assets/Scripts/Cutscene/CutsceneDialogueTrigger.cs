using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueTrigger : MonoBehaviour
{
    public void StartDialogue(DialogueObject dialogueObject)
    {
        UICaller uicaller = GlobalReferences.UIcaller;
        if (!uicaller)
            return;

        DialogueUI dialogueui = GlobalReferences.Dialogueui;
        if (!dialogueui)
            return;

        ResponseEvent[][] temp = CompileResponseEvents();
        //ResponseEvent[][] temp = new ResponseEvent[1][];

        dialogueui.OnDialogueEnd += OnDialogueEnd;
        uicaller.CallShowDialogue(dialogueObject, temp);
    }

    private void OnDialogueEnd()
    {
        TimelineManagerNew timelineManager = GlobalReferences.TimelineManager;
        if (!timelineManager)
            return;

        timelineManager.ResumeCutscene();

        DialogueUI dialogueui = GlobalReferences.Dialogueui;
        if (!dialogueui)
            return;

        dialogueui.OnDialogueEnd -= OnDialogueEnd;
    }

    private ResponseEvent[][] CompileResponseEvents()
    {
        ResponseEvent[][] compResponseEvents = new ResponseEvent[GetComponents<DialogueResponseEvents>().Length][];
        int tempIndex = 0;
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            compResponseEvents[tempIndex] = responseEvents.Events;
            tempIndex++;
        }
        return compResponseEvents;
    }
}