using System.Collections;
using System.Collections.Generic;

using System.Collections;
using UnityEngine;

using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private TypewriterEffect typewriterEffect;
    private void Start()
    {
        //textLabel.text = "Helle\n2nd line";
        //GetComponent<TypewriterEffect>().Run(textToType: "This is some text", textLabel);
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(routine: StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        yield return new WaitForSeconds(2);


        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            //
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); //wont go to next dialogue until user hits spacebar

            //keeps looping as long as there is some dialogue to show
        }
        CloseDialogueBox(); 
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
