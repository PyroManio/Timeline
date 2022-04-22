using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneEffects : MonoBehaviour
{
    public void FadeIn()
    {
        GlobalReferences.Fader.FadeIn();
    }

    public void FadeOut()
    {
        GlobalReferences.Fader.FadeOut();
    }

    public void ShakeCamera()
    {
        StartCoroutine(GlobalReferences.CameraShake.Shake(1f, 0.4f));
    }

    public void SetDespairActive(bool active)
    {
        GlobalReferences.Despair.SetSpriteActive(active);
    }

    public void FadeInResume()
    {
        GlobalReferences.Fader.FadeIn();
        GlobalReferences.Fader.OnFadeInComplete += ResumeCutsceneAfterFadeIn;
    }

    public void FadeOutResume()
    {
        GlobalReferences.Fader.FadeOut();
        GlobalReferences.Fader.OnFadeOutComplete += ResumeCutsceneAfterFadeOut;
    }

    public void SetPlayerIdleAnimation(string direction)
    {
        direction = direction.ToLower();

        GlobalReferences.Player.GetComponent<Animator>().SetFloat("LastVertical", 0);
        GlobalReferences.Player.GetComponent<Animator>().SetFloat("LastHorizontal", 0);

        switch (direction)
        {
            case "up":
                {
                    GlobalReferences.Player.GetComponent<Animator>().SetFloat("LastVertical", 1f);
                    break;
                }
            case "down":
                {
                    GlobalReferences.Player.GetComponent<Animator>().SetFloat("LastVertical", -1f);
                    break;
                }
            case "right":
                {
                    GlobalReferences.Player.GetComponent<Animator>().SetFloat("LastHorizontal", 1f);
                    break;
                }
            case "left":
                {
                    GlobalReferences.Player.GetComponent<Animator>().SetFloat("LastHorizontal", -1f);
                    break;
                }
        }
    }

    public void SetDespairAnimation(string animation)
    {
        GlobalReferences.Despair.GetComponent<Animator>().Play(animation);
    }

    private void ResumeCutsceneAfterFadeIn()
    {
        GlobalReferences.TimelineManager.ResumeCutscene();
        GlobalReferences.Fader.OnFadeInComplete -= ResumeCutsceneAfterFadeIn;
    }

    private void ResumeCutsceneAfterFadeOut()
    {
        GlobalReferences.TimelineManager.ResumeCutscene();
        GlobalReferences.Fader.OnFadeOutComplete -= ResumeCutsceneAfterFadeOut;
    }


}
