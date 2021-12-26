using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;
    private ResponseEvent[][] responseEventsList;
    private DialogueObject currentDialogue;
    private List<GameObject> tempResponseButtons = new List<GameObject>();
    [SerializeField] private Inventory inventory;
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }
    public void AddResponseEvents(ResponseEvent[] responseEvents){
        this.responseEvents = responseEvents;
    }
    public void AddResponseEventsList(ResponseEvent[][] givenEvents)
    {
        ClearResponseEventsList();
        Debug.Log(givenEvents);
        this.responseEventsList = givenEvents;
    }
    public void ClearResponseEventsList()
    {
        responseEventsList = null;
    }

    public void ShowResponses(Response[] responses)
    {
        int longestResponseLength=0;
        float responseBoxHeight=0;
        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            // If a response needs an item to show, and that item is NOT in the inventory, don't include this response
            // I think there might be a problem with this if there's an instance where a dialogue object has
            // Only one response that requires an item, and that item isn't in the inventory. The response
            // box will attempt to show, however, there will be no responses. Just try to have at least
            // one non-item required response for any of the responses with a dialogue object
            if (response.NeedsItem && !inventory.HasItem(response.RequiredItem)) continue;
            
            int responseIndex = i;
            if (response.ResponseText.Length > longestResponseLength) longestResponseLength = response.ResponseText.Length;
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText; 
            responseButton.GetComponent<Button>().onClick.AddListener( () => OnPickedResponse(response, responseIndex));
            tempResponseButtons.Add(responseButton);
            responseBoxHeight += responseButtonTemplate.sizeDelta.y+ 10;
        }
        //responseBox.sizeDelta=new Vector2(responseBox.sizeDelta.x+(longestResponseLength*20-100),responseBoxHeight);
        responseBox.sizeDelta = new Vector2(Mathf.Sqrt(longestResponseLength * 7000),responseBoxHeight +10);
        responseBox.gameObject.SetActive(true);
    } 
    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        Debug.Log(response.DTag);
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
        //if (responseEvents !=null && responseIndex <= responseEvents.Length) {responseEvents[responseIndex].OnPickedResponse?.Invoke();}
        if (responseEventsList != null)
            foreach (ResponseEvent[] responEvent in responseEventsList)  foreach (ResponseEvent rEvent in responEvent) 
            {
                Debug.Log(rEvent.DTag);
                if (rEvent.DTag.Equals(response.DTag))  responEvent[responseIndex].OnPickedResponse?.Invoke();
            }
    
        responseEvents = null;
        //responseEventsList = null;
        if (response.DialogueObject){
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }
}
