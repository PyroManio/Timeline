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
    public void changeScreen(int index){
        for (int i=0; i<screens.Length; i++) screens[i].SetActive(false);
        screens[index].SetActive(true);
    }
}
