using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;  //basically creates new field in unity called dialogue
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue;   //prevents code from outside to write to it (no overwrite)

    //
    new public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
