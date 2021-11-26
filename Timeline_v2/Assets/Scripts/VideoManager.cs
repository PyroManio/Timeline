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
        videoStuff.Play();
        StartCoroutine(waitTillEnd());
    }
    private IEnumerator waitTillEnd()
    {
        if (!videoStuff.isPrepared)
            yield return null;
        Debug.Log(videoStuff.isPlaying);
        while(videoStuff.isPlaying)
        {
            player.inCutscene = true;
            yield return null;
        }
        for (int i = 0; i < 300; i++) yield return null;
        player.inCutscene = false;
        finishVideo.Invoke();
        Debug.Log("video enede");
        videoRenderer.SetActive(false);
    }
}
