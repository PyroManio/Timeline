using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    void Start()
    {
        changeScreen(0);
    }
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            screens[0].SetActive(false);
            screens[1].SetActive(true);
            //screens[1].TryGetComponent
            //screens[1].GetComponentInChildren<PuzzleScreen>().setCamera();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            screens[0].SetActive(true);
            screens[1].SetActive(false);
        }
    }
    public void changeScreen(int index){
        for (int i=0; i<screens.Length; i++) screens[i].SetActive(false);
        screens[index].SetActive(true);
    }
}
