using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject screenObject;
    public bool IsOpen { get; private set; }
    public void closeScreen(){
        IsOpen=false;
        screenObject.SetActive(false);
    }
    public void openScreen(){
        IsOpen=true;
        screenObject.SetActive(true);
    }
    void Start()
    {
        closeScreen();
    }
}
