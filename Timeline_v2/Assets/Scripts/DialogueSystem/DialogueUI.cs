using System.Collections;
using UnityEngine;
using TMPro;
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueObject testDialogue;

    //NEW
    public bool IsOpen { get; private set; }
    
    private ResponseHandler responseHandler;
    private TypewriterEffect typewritterEffect;
    private void Start()
    {
        typewritterEffect=GetComponent<TypewriterEffect>();
        responseHandler=GetComponent<ResponseHandler>();
        CloseDialogueBox();
        //ShowDialogue(testDialogue);
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        //NEW
        IsOpen = true; 

        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
       // foreach (string dialogue in dialogueObject.Dialogue)
        //{
        //    yield return typewritterEffect.Run(dialogue,textLabel);
        //    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        //}
        for (int i =0; i<dialogueObject.Dialogue.Length;i++)
        {
            string dialogue= dialogueObject.Dialogue[i];
            yield return typewritterEffect.Run(dialogue,textLabel); 

            if (i == dialogueObject.Dialogue.Length-1 && dialogueObject.HasResponses) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
        
    }
    private void CloseDialogueBox(){
        //NEW
        IsOpen = false;

        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }
}
