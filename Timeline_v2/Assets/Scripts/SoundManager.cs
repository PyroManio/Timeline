using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] musicList;
    [SerializeField] private AudioSource[] soundList;
    private int currentPlayingMusicIndex=-1;
    private bool IsFading;
    private float tempVol;
    public void PlayMusic(int index){
        Debug.Log("playing"+index);
        if (IsFading) 
        {
            StopAllCoroutines();
            musicList[currentPlayingMusicIndex].volume = tempVol;
            IsFading = false;
            Debug.Log("QUICK STOP FADING");
        }
        if (currentPlayingMusicIndex!=-1) 
        {
            musicList[currentPlayingMusicIndex].Stop();
        }
        currentPlayingMusicIndex = index;
        musicList[index].Play();
    }
    public void StopMusic()
    {
        Debug.Log("stopped"+currentPlayingMusicIndex);
        if (currentPlayingMusicIndex == -1) return;
        musicList[currentPlayingMusicIndex].Stop();
        currentPlayingMusicIndex =- 1;
    }
    public void FadeStopMusic()
    {  
        Debug.Log("stopping"+currentPlayingMusicIndex);
        if (currentPlayingMusicIndex == -1) return;
        StartCoroutine(fadingStop());
    }
    public void PlaySound(int index)
    {
        soundList[index].Play();
    }
    private IEnumerator fadingStop()
    {
        IsFading = true;
        tempVol = musicList[currentPlayingMusicIndex].volume;
        yield return null;
        while (musicList[currentPlayingMusicIndex].volume > 0)
        {
            musicList[currentPlayingMusicIndex].volume += -0.006f;
            if (musicList[currentPlayingMusicIndex].volume < 0) musicList[currentPlayingMusicIndex].volume=0;
            yield return null;
        }
        musicList[currentPlayingMusicIndex].volume = tempVol;
        StopMusic();
        IsFading = false;
        Debug.Log("volll");
    }
}
