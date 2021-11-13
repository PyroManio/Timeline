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
    private TimelineAsset currentTimeline;
    public void Play(){
        playableDirector.Play();
    }
    public void ChangeTimeline(TimelineAsset givenTimeline, bool loop)
    {
        //GetComponent<PlayableDirector>().playableAsset=givenTimeline;
        //GetComponent<PlayableDirector>().;
        //Debug.Log(WrapMode);
        //WrapMode.Loop;
        //GetComponent<PlayableDirector>().
        isLoop=loop;

    }
    public void PlayTimeline(TimelineAsset givenTimeline, bool loop)
    {
        timelineEnd=false;
        isLoop=loop;
        currentTimeline=givenTimeline;
        playableDirector.Play(currentTimeline);
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
        if (isLoop) playableDirector.Play(currentTimeline);
    }
    public void stopTimeline()
    {
        playableDirector.Stop();
    }
}
