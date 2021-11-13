using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public bool timelineEnd=false;
    public bool isLoop;
    public void Play(){
        playableDirector.Play();
    }
    public void ChangeTimeline(TimelineAsset givenTimeline, bool loop)
    {
        GetComponent<PlayableDirector>().playableAsset=givenTimeline;
        //GetComponent<PlayableDirector>().;
        //Debug.Log(WrapMode);
        //WrapMode.Loop;
        //GetComponent<PlayableDirector>().
        isLoop=loop;
    }
    public bool isPlaying(){
        if (timelineEnd) 
        {
            timelineEnd=false;
            return false;
        }
        return true;
    }
    public void timelineEnded()
    {
        timelineEnd=true;
        if (isLoop) playableDirector.Play();
    }
    public void stopTimeline()
    {
        playableDirector.Stop();
    }
}
