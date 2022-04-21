using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

[System.Serializable]
public class DialogueEvent: UnityEvent<DialogueObject>
{

}


public class TimelineManagerNew : MonoBehaviour
{
    private PlayableDirector pd;
    private double duration, currentState;

    [SerializeField] private UnityEvent[] callbacks;
    [SerializeField] private DialogueEvent[] dialogues;

    private void Awake()
    {
        GlobalReferences.TimelineManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    public void SetPlayableAsset(PlayableAsset playableAsset)
    {
        pd.playableAsset = playableAsset;
        ResetInitialState();
    }

    public void PlayCutscene()
    {
        pd.Play();
    }

    public void StopCutscene()
    {
        pd.Stop();
    }

    public void PauseCutscene(double duration)
    {
        this.duration = duration;
        currentState = pd.time;
        pd.Stop();
    }

    public void ResumeCutscene()
    {
        pd.initialTime = currentState + duration;
        pd.Play();
    }

    public void InvokeCallback(int index)
    {
        try
        {
            callbacks[index]?.Invoke();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Callback function does not exist. Check the callback index");
        }
    }

    public void TriggerDialogue(int index, DialogueObject dialogueObject)
    {
        try
        {
            dialogues[index]?.Invoke(dialogueObject);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Dialogue function does not exist. Check the dialogue index");
        }
    }

    public void ResetInitialState()
    {
        pd.initialTime = 0;
    }
}
