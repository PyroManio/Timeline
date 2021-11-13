using System.Collections;
using UnityEngine;
using TMPro;
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    
    [SerializeField] private GameObject characterDialogueBox;
    //[SerializeField] private DialogueObject testDialogue;
    [SerializeField] private TMP_Text defaultText;
    private TMP_Text textLabel;
    //NEW
    public bool IsOpen { get; private set; }
    
    private ResponseHandler responseHandler;
    private TypewriterEffect typewritterEffect;
    private void Start()
    {
        textLabel=defaultText;
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
        textLabel.text=string.Empty;
        textLabel=defaultText;
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    //returns edited string without the %. If it turns out this isn't a character dialogue, return the normal dialouge
    private string expressionDialogue(string dialogue){
        //first we have to check if this is an actual expression trigger or someone decided to put the first text as a % for some reason
        for (int i = 1; i < dialogue.Length; i++)
        {
           
            if ( System.String.Equals(dialogue[i],'%'))
            {
                //checks to see if it isn't a %% thing or %heh% thing, although I have to question why are you putting this into the dialogue
                if (i!=1 && int.TryParse(dialogue.Substring(1,i-1),out int number))
                    characterDialogueBox.GetComponentInChildren<ExpressionDialogueSprite>().changeExpression(int.Parse(dialogue.Substring(1,i-1)));
                characterDialogueBox.SetActive(true);
                textLabel = characterDialogueBox.GetComponentInChildren<TMP_Text>();
                dialogue=dialogue.Substring(i+1);
                break;
            }
        }
        return dialogue;
        
    }
    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    public IEnumerator InTheLoop(DialogueObject dialogueObject)
    {
        ShowDialogue(dialogueObject);
        StepThroughDialogue(dialogueObject);
        yield return null;
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i =0; i<dialogueObject.Dialogue.Length;i++)
        {
            //assume all dialogue is default dialogue until proven otherwise
            textLabel.text=string.Empty;
            textLabel = defaultText;
            characterDialogueBox.SetActive(false);
            string dialogue= dialogueObject.Dialogue[i];
            // if there is a % at the beginning of the dialogue
            if (System.String.Equals(dialogueObject.Dialogue[i][0],'%'))  dialogue = expressionDialogue(dialogueObject.Dialogue[i]);
            //yield return typewritterEffect.Run(dialogue,textLabel); 
            yield return RunTypingEffect(dialogue);
            textLabel.text=dialogue;
            if (i == dialogueObject.Dialogue.Length-1 && dialogueObject.HasResponses) break;
            yield return null;
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
    private IEnumerator RunTypingEffect(string dialogue)
    {   
        typewritterEffect.Run(dialogue, textLabel);
        while (typewritterEffect.IsRunning){
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewritterEffect.Stop();
            }
        }
    }
    public void CloseDialogueBox(){
        //NEW
        IsOpen = false;
        characterDialogueBox.SetActive(false);
        dialogueBox.SetActive(false);
        defaultText.text=string.Empty;
        textLabel.text=string.Empty;
        characterDialogueBox.GetComponentInChildren<TMP_Text>().text=string.Empty;
    }
}
