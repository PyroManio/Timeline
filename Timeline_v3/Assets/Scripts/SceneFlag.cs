using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
[System.Serializable]
public class SceneFlag 
{
    [SerializeField] public UnityEvent[] eventFlag;
    [SerializeField] public FlagNames nickName;
    public void ActivateFlagEvent(int index){
        if (index < eventFlag.Length )  eventFlag[index]?.Invoke();
        else Debug.Log("WARNING-Flag Event out of range. Flag called: " + nickName + " | Flag Index called: "+index);
    }

}

//First name, room (BED, HALL,BATH, etc.), if its in an inspectoin screen, say what inspec screne it is (NS,CLOS,DRAW)
// Then name of object first and then the action its performing. If it involves multiple objects, generally describe what happens


[Serializable]
public enum FlagNames{
    DEFAULT,
    BED_NS_DrawerStatus, // 0 = screws on, 1 = screws removed, 2 = opened
    BED_DRAW_Screwdriver //0 = not picked up/visible, 1 = picked up / not visible
}