using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class ScreenHandler : MonoBehaviour
{   

    [SerializeField] GameObject[] screens;
    [SerializeField] private TransitionScreen transitionScreen;
    [SerializeField] string[] sceneList;
    void Start()
    {
        //changeScreen(0);
    }
    public void changeScreen(int index){
        for (int i=0; i<screens.Length; i++) screens[i].SetActive(false);
            screens[index].SetActive(true);
        //this is genuinely the worst fix to this stupid damn problem but honestly i am in rage rn
        Debug.Log("FF");
        transitionScreen.StopAllCoroutines();
        transitionScreen.halfTransitionItself();
    }
    public void transChangeScreen(int index)
    {
        transitionScreen.fullTransitionItself((index));
    }
    public void changeScene(int index)
    {
        //Debug.Log("brrgrg");
        SceneManager.LoadScene(sceneList[index]);
    }
    public void transChangeScene(int index)
    {
        transitionScreen.sceneTransition(index);
    }

}
