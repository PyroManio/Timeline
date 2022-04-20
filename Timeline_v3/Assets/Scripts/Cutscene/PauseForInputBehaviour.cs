using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;
using UnityEngine.Events;

[Serializable]
public class PauseForInputBehaviour : PlayableBehaviour
{
    [SerializeField] private int callbackIndex;
    public double start, end;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        GameObject timelineManager = playerData as GameObject;

        if (!timelineManager)
            return;

        
        TimelineManagerNew timeline = timelineManager.GetComponent<TimelineManagerNew>();

        timeline.InvokeCallback(callbackIndex);
        timeline.PauseCutscene(end - start);
    }
}
