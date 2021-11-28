using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
public class VideoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoStuff;
    [SerializeField] private UnityEvent finishVideo;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject videoRenderer;
    void Start()
    {
        videoStuff.Prepare();
    }
    public void PlayVideo()
    {
        videoRenderer.SetActive(true);
        
        StartCoroutine(waitTillEnd());
    }
    private IEnumerator waitTillEnd()
    {
        if (!videoStuff.isPrepared)
            yield return null;
        videoStuff.Play();
        Debug.Log(videoStuff.isPlaying);
        while(true)
        {
            player.inCutscene = true;
            yield return null;
            if (!videoStuff.isPlaying)
            {
                yield return null;
                if (!videoStuff.isPlaying) break;
            }

        }
        for (int i = 0; i < 300; i++) yield return null;
        player.inCutscene = false;
        finishVideo.Invoke();
        Debug.Log("video enede");
        videoRenderer.SetActive(false);
    }
}
