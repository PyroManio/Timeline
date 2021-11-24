using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ScreenHandler : MonoBehaviour
{   

    [SerializeField] GameObject[] screens;
    [SerializeField] private TransitionScreen transitionScreen;
    void Start()
    {
        //changeScreen(0);
    }
    public void changeScreen(int index){
        for (int i=0; i<screens.Length; i++) screens[i].SetActive(false);
            screens[index].SetActive(true);
        //this is genuinely the worst fix to this stupid damn problem but honestly i am in rage rn
        transitionScreen.StopAllCoroutines();
        transitionScreen.halfTransitionItself();
    }
    public void transChangeScreen(int index)
    {
        transitionScreen.fullTransitionItself((index));
    }

}
