using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Cutscene/CutsceneObject")]
public class CutsceneObject : ScriptableObject
{
    [SerializeField] private MiniCutObject[] cutsceneFlow;
    public MiniCutObject[] CutsceneFlow => cutsceneFlow;
}
