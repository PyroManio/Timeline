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
    [SerializeField] SoundManager soundManager;
    private CharacterTalking currentTalking; // 0=none, 1=Leo, 2=Despair 
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
    private void SetSpecialDialogue()
    {
        specialDialogueBox.SetActive(true);
        textLabel=specialDialogueBox.GetComponentInChildren<TMP_Text>();
        specialBox=true;
    }

    //returns edited string without the %. If it turns out this isn't a character dialogue, return the normal dialouge
    private void SetCharacterBox(DialogueTextData givenData){
        characterDialogueBox.GetComponentInChildren<ExpressionDialogueSprite>().changeExpression(givenData.CharSpeaking, givenData.CharExprssion);
         characterDialogueBox.SetActive(true);
         textLabel = characterDialogueBox.GetComponentInChildren<TMP_Text>();
         currentTalking = givenData.CharSpeaking;
    }

    private string checkForInfo(string dialogue){
        //bool firstFound=false;
        int firstIndex=-1;
        int secondIndex=-1;
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (System.String.Equals(dialogue[i],'%'))
            {
                if (firstIndex!=-1) {
                    secondIndex = i;
                    break;
                }
                else firstIndex = i; 
            }
        }
        if (secondIndex==-1) return dialogue;
        
        if (dialogue.Substring(firstIndex + 1,secondIndex-firstIndex-1 ).Equals("PlayerName"))
        {
            dialogue=dialogue.Substring(0,firstIndex) + infoGet.playerName + dialogue.Substring(secondIndex+1,dialogue.Length-secondIndex-1);
        }
        else if (dialogue.Substring(firstIndex+1,2).Equals("SE"))
        {
            soundManager.PlaySound(int.Parse(dialogue.Substring(firstIndex + 3, secondIndex-firstIndex-3)));
            dialogue=dialogue.Substring(0,firstIndex) + dialogue.Substring(secondIndex+1,dialogue.Length-secondIndex-1);
            //dialogue=dialogue.Substring(0,firstIndex);
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
        for (int i =0; i < dialogueObject.DialogueData.Length;i++)
        {
            DialogueTextData dialogueStuff = dialogueObject.DialogueData[i];
            //assume all dialogue is default dialogue until proven otherwise
            textLabel.text = string.Empty;
            currentTalking = dialogueStuff.CharSpeaking;
            if (!dialogueObject.IsSpecialDialogue) textLabel = defaultText;
            else SetSpecialDialogue();
            if (characterDialogueBox != null)  characterDialogueBox.SetActive(false);
            string dialogue = dialogueStuff.DialogueText;
            if (dialogueStuff.CharSpeaking == CharacterTalking.Leo && !specialBox) SetCharacterBox(dialogueStuff);
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (!currentTalking.Equals(CharacterTalking.None)) soundManager.StopSounds();
            if (i == dialogueObject.Dialogue.Length-1 && dialogueObject.HasResponses) break;
            yield return null;
            if (!forceContinue)
            {
                if (forceStay)
                {
                    yield return new WaitUntil(() => forceContinue);
                    forceStay = false;
                }
                else yield return new WaitUntil(() => forceContinue||Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0));
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
        if (dialogueObject.HasNextDialogue) ShowDialogue(dialogueObject.NextDialgoue);  
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {   
        typewritterEffect.Run(dialogue, textLabel, currentTalking);
        while (typewritterEffect.IsRunning){
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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
