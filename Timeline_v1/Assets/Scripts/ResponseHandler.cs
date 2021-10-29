
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();
    
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    //main driver
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

        foreach(Response response in responses) //top 10 lines of code that may make a programmer throw up
        {
           GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
           responseButton.gameObject.SetActive(true);
           responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
           responseButton.GetComponent<Button>().onClick.AddListener(call:() => OnPickedResponse(response));

            //
            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;

        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, y: responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }
    private void OnPickedResponse(Response response)
    {
        //setting up yes/no/response 
        responseBox.gameObject.SetActive(false);

        foreach(GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        dialogueUI.ShowDialogue(response.DialogueObject);
    }
}
