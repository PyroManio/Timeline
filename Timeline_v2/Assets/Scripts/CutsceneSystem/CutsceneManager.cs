using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Events;
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] TimelineManager timelineManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObject screenManager;

    private bool cutscenePlaying=false;
    private CutsceneGameObject currentCutsceneObject;
    private CutsceneObject currentCutscene;
    private UnityEvent[] cutsceneEventList;
    public void PlayCutscene(CutsceneObject cutsceneStuff){
        //timelineManager.Play();
        cutscenePlaying=true;
        currentCutscene=cutsceneStuff;
        player.GetComponent<PlayerMovement>().inCutscene=true;
        StartCoroutine(StepThroughCutscene());
    }
    public void PlayCutscene(CutsceneGameObject cutsceneStuff){
        //timelineManager.Play();
        currentCutsceneObject = cutsceneStuff;
        cutsceneEventList = cutsceneStuff.CutsceneEvents;
        PlayCutscene(cutsceneStuff.CutsceneObject);
    }
    private IEnumerator StepThroughCutscene()
    {
        for (int i = 0; i < currentCutscene.CutsceneFlow.Length; i++)
        {
            MiniCutObject currentMini = currentCutscene.CutsceneFlow[i];
            if (currentMini.HasTimeline)
            {
                yield return (PlayTimeline(currentMini.TimelineObject, currentMini.IsLoop));
            }
            else if (currentMini.HasDialogue)
            {
                yield return (PlayDialogue(currentMini.dialogue));
            }
            else if (currentMini.hasEvent && currentMini.eventIndex < cutsceneEventList.Length) 
            {
                cutsceneEventList[currentMini.eventIndex].Invoke();
            }
            
        }
        player.GetComponent<PlayerMovement>().inCutscene=false;
        cutscenePlaying=false;
        timelineManager.stopTimeline();
        Debug.Log("Cutscene Ended");
    }
    //private IEnumerator PlayEvent
    private IEnumerator PlayTimeline(TimelineAsset givenTimeline, bool isLoop)
    {
        //Debug.Log(timelineManager.isPlaying);
        yield return null;
        timelineManager.stopTimeline();
        //timelineManager.ChangeTimeline(givenTimeline, isLoop);
        //timelineManager.Play();
        timelineManager.PlayTimeline(givenTimeline, isLoop,0);
        //give this to the timeliine manager to play it???
        while (timelineManager.isPlaying())
        {
            yield return null;
        }
    }
    private IEnumerator PlayDialogue(DialogueObject givenDialogue)
    {
        yield return null;
        dialogueUI.CloseDialogueBox();
        dialogueUI.ShowDialogue(givenDialogue);
       //yield return dialogueUI.InTheLoop(givenDialogue);
        //yield return null;
        
        while(dialogueUI.IsOpen)
        {
            yield return null;
        }
    }
    
}
