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
    [SerializeField] private GameObject specialDialogueBox;
    private ResponseHandler responseHandler;
    private TypewriterEffect typewritterEffect;
    public bool specialBox=false;
    private bool forceContinue = false;
    private bool forceStay = false;
    [SerializeField] InfoRemember infoGet;
    public void ForceContiue()
    {
        forceContinue = true;
    }
    public void ForceStay()
    {
        forceStay = true;
    }
    private void Start()
    {
        textLabel=defaultText;
        typewritterEffect=GetComponent<TypewriterEffect>();
        responseHandler=GetComponent<ResponseHandler>();
        CloseDialogueBox();
        //ShowDialogue(testDialogue);
    }
    public void nextSpecial(){
        specialBox = true;
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        //NEW
        if (specialBox) {
            ShowSpecialDialogue(dialogueObject);
            return;
        }
        IsOpen = true; 
        dialogueBox.SetActive(true);
        textLabel.text=string.Empty;
        textLabel=defaultText;
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }
    public void ShowSpecialDialogue(DialogueObject dialogueObject)
    {
        //NEW
        IsOpen = true; 
        dialogueBox.SetActive(true);
        textLabel.text=string.Empty;
        specialDialogueBox.SetActive(true);
        textLabel=specialDialogueBox.GetComponentInChildren<TMP_Text>();
        specialBox=true;
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
    private string checkForInfo(string dialogue){
        if (System.String.Equals(dialogue[0],'%'))  dialogue = expressionDialogue(dialogue);
        //bool firstFound=false;
        int firstIndex=-1;
        int secondIndex=-1;
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (System.String.Equals(dialogue[i],'%'))
            {
                if (firstIndex!=-1) {
                    secondIndex=i;
                    break;
                }
                else
                {
                    //firstFound=true;
                    firstIndex=i;
                }
            }
        }
        if (secondIndex==-1) return dialogue;
        
        if (dialogue.Substring(firstIndex + 1,secondIndex-firstIndex-1 ).Equals("PlayerName"))
        {
            dialogue=dialogue.Substring(0,firstIndex) + infoGet.playerName + dialogue.Substring(secondIndex+1,dialogue.Length-secondIndex-1);
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
            if (!specialBox) textLabel = defaultText;
            if (characterDialogueBox!=null)
                characterDialogueBox.SetActive(false);
            string dialogue = dialogueObject.Dialogue[i];
            // if there is a % at the beginning of the dialogue
                //if (System.String.Equals(dialogueObject.Dialogue[i][0],'%'))  dialogue = expressionDialogue(dialogueObject.Dialogue[i]);
            dialogue = checkForInfo(dialogueObject.Dialogue[i]);
            //yield return typewritterEffect.Run(dialogue,textLabel); 
            yield return RunTypingEffect(dialogue);
            textLabel.text=dialogue;
            if (i == dialogueObject.Dialogue.Length-1 && dialogueObject.HasResponses) break;
            yield return null;
            if (!forceContinue)
            {
                if (forceStay)
                {
                    yield return new WaitUntil(() => forceContinue);
                    forceStay = false;
                }
                else yield return new WaitUntil(() => forceContinue||Input.GetKeyDown(KeyCode.Space));
            }
            forceContinue = false;
        }
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
        if (specialBox) specialDialogueBox.SetActive(false);
        specialBox = false;
        
        
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
        if (characterDialogueBox!=null)
            characterDialogueBox.SetActive(false);
        dialogueBox.SetActive(false);
        defaultText.text=string.Empty;
        textLabel.text=string.Empty;
        if (characterDialogueBox!=null)
            characterDialogueBox.GetComponentInChildren<TMP_Text>().text=string.Empty;
    }
}
