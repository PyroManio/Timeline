using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RoomSceneManager : MonoBehaviour
{
    // One per scene room
    public static event Action<DialogueObject, ResponseEvent[][]> ShowDialogue;
    public void CallShowDialogue(DialogueObject givenDialogue,ResponseEvent[][] givenResponseEvents)
    {
        ShowDialogue?.Invoke(givenDialogue,givenResponseEvents);
    }
    
}
