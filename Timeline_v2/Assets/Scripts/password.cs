using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class password : MonoBehaviour
{
    public InputField PasswordInput;

    public void CheckPasswordCondition()
    {
        string ReceivedString = PasswordInput.text;

        if(ReceivedString == "SchrodingersCat")
        {
            Debug.Log("correct password");

        }
        else
        {
            Debug.Log("incorrect password");
        }
    }
}
