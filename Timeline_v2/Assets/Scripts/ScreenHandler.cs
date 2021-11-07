using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    void Start()
    {
        screens[0].SetActive(true);
        screens[1].SetActive(false);
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
}
