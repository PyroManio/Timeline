using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ClickableObjectHandler : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private GameObject[] clickableObjects;
    [SerializeField] private UnityEvent[] objectEvents;
    public bool imOutOfVariableNames = true;
    public bool dialogueOpen => dialogueUI.IsOpen;
    // Start is called before the first frame update
    private void Start()
    {
        for (int i=0; i < clickableObjects.Length; i++){
            //WHY IN THE WORLD IS THE LAST I ENDING UP AS A 1???? HOW THE HELL
            int num = i;
            clickableObjects[i].GetComponentInChildren<Button>().onClick.AddListener( () => OnPickedResponse(num));
        }
    }

    private void OnPickedResponse(int index){
        if (!imOutOfVariableNames) return;
        if (dialogueOpen) return;
        if (index >= objectEvents.Length) return;
        //Debug.Log();
        //if (!dialogueUI.IsOpen && index < objectEvents.Length) objectEvents[index].Invoke();
        objectEvents[index].Invoke();
    }
}
