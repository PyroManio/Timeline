using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;
using UnityEngine.Events;

[Serializable]
public class PauseForInputBehaviour : PlayableBehaviour
{
    [SerializeField] private bool invokeCallback = false;
    [SerializeField] private int callbackIndex;
    [SerializeField] private bool triggerDialogue = false;
    [SerializeField] private int dialogueIndex;
    [SerializeField] private bool triggerNpcAnimation = false;
    [SerializeField] private int animationIndex;
    [NonSerialized] public double start, end;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        GameObject timelineManager = playerData as GameObject;

        if (!timelineManager)
            return;

        
        TimelineManagerNew timeline = timelineManager.GetComponent<TimelineManagerNew>();

        if(invokeCallback)
            timeline.InvokeCallback(callbackIndex);
        if(triggerDialogue)
        {
            DialogueObject dialogueObject = ScriptableObject.CreateInstance("DialogueObject") as DialogueObject;
            timeline.TriggerDialogue(dialogueIndex, dialogueObject);
        }
        if (triggerNpcAnimation)
        {
            Debug.Log("trigger animation");
            timeline.TriggerNpcAnimation(animationIndex, new Animator());
        }

        timeline.PauseCutscene(end - start);
    }
}
