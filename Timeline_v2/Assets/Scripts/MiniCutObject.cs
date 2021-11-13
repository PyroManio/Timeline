using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
[CreateAssetMenu(menuName ="Cutscene/MiniCutObject")]
public class MiniCutObject : ScriptableObject
{
   //this is so stupid, why can't i just have a simple array list that can hold both timelines and dialogueobjects
   [SerializeField] private DialogueObject dialogueObject;
   //[SerializeField] private TimelinePreferences timelineObject;
   [SerializeField] private TimelineAsset timelineObject;
   [SerializeField] private bool isLoop;
   public bool HasTimeline => timelineObject != null;
   public bool HasDialogue => dialogueObject != null;
   public bool IsLoop => isLoop;
   public DialogueObject dialogue => dialogueObject;
   public TimelineAsset TimelineObject => timelineObject;
}
