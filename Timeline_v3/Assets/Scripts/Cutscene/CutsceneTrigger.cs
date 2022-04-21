using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableAsset playableAsset;
    private TimelineManagerNew timelineManager;

    private void Awake()
    {
        timelineManager = FindObjectOfType<TimelineManagerNew>();
    }

    public void StartCutscene()
    {

    }
}
