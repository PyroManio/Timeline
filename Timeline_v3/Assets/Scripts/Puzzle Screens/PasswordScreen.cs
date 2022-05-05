using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class PasswordScreen : MonoBehaviour
{
    //[SerializeField] private InputField passwordField;
    [SerializeField] private GameObject passScreen;
    
    [SerializeField] private string correctPassword;
    [SerializeField] private UnityEvent correctEvent;
    [SerializeField] private UnityEvent wrongEvent;
    private string passwordSubmit;
    [SerializeField] private string promptText =  "Enter Password...";
    public bool IsOpen { get; private set; }
    void Start(){
        passScreen.GetComponentInChildren<TMP_InputField>().placeholder.GetComponentInChildren<TMP_Text>().text = promptText;
        CloseScreen();
    }
    public void OnSubmit()
    {
        passwordSubmit=passScreen.GetComponentInChildren<TMP_InputField>().text;
        if (passwordSubmit.Equals(correctPassword)) correctEvent.Invoke();
        else 
        {
            passScreen.GetComponentInChildren<TMP_InputField>().placeholder.GetComponentInChildren<TMP_Text>().text = promptText;
            passScreen.GetComponentInChildren<TMP_InputField>().text = "";
            wrongEvent.Invoke();
        }
        CloseScreen();
    }
    public void OpenScreen(){
        IsOpen=true;
        passScreen.SetActive(true);
    }
    public void CloseScreen(){
        IsOpen=false;
        passScreen.SetActive(false);
    }
}
