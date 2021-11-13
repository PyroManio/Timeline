using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] TimelineManager timelineManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObject screenManager;

    private bool cutscenePlaying=false;
    private string currentTypePlaying="";
    private CutsceneObject currentCutscene;
    public void PlayCutscene(CutsceneObject cutsceneStuff){
        //timelineManager.Play();
        cutscenePlaying=true;
        currentCutscene=cutsceneStuff;
        player.GetComponent<PlayerMovement>().inCutscene=true;
        
        StartCoroutine(StepThroughCutscene());
    }
    private IEnumerator StepThroughCutscene()
    {
        for (int i = 0; i < currentCutscene.CutsceneFlow.Length; i++)
        {
            if (currentCutscene.CutsceneFlow[i].HasTimeline)
            {
                currentTypePlaying="timeline";
                yield return (PlayTimeline(currentCutscene.CutsceneFlow[i].TimelineObject, currentCutscene.CutsceneFlow[i].IsLoop));
            }
            else if (currentCutscene.CutsceneFlow[i].HasDialogue)
            {
                currentTypePlaying="dialogue";
                yield return (PlayDialogue(currentCutscene.CutsceneFlow[i].dialogue));
            }
            
        }
        
        player.GetComponent<PlayerMovement>().inCutscene=false;
        cutscenePlaying=false;
        timelineManager.stopTimeline();
        Debug.Log("Cutscene Ended");
    }
    private IEnumerator PlayTimeline(TimelineAsset givenTimeline, bool isLoop)
    {
        yield return null;
        timelineManager.ChangeTimeline(givenTimeline, isLoop);
        timelineManager.Play();
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
