using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UICaller : MonoBehaviour
{
    // This helps bridge between the regular scene and the UI scene 
    // This calls out to the object handling all of the UI's in the other scene.
    public bool areUIOpen {get; private set;}
    public UIName CurrentUIOpened;
    public bool IsInspectonScreensOpen = false;
    public static event Action<DialogueObject, ResponseEvent[][]> ShowDialogue;
    public static event Action<UIName> OpenUIMenu;
    public static event Action<UIName> CloseUIMenu;
    public static event Action<UIName,Inventory> InventoryMainToUI;
    // This calls out to the UIManager and passes over dialogue data to it
    public void CallShowDialogue(DialogueObject givenDialogue,ResponseEvent[][] givenResponseEvents)
    {
        ShowDialogue?.Invoke(givenDialogue,givenResponseEvents);
    }
    public void CallOpenMenu (UIName givenUI)
    {
        OpenUIMenu?.Invoke(givenUI);
    }
    public void CallCloseMenu (UIName givenUI)
    {
        CloseUIMenu?.Invoke(givenUI);
    }
    private void Awake(){
        UIManager.AreUIOpen += Update_AreUIOpen;
        UIManager.GetInventory += SendInventoryToUIScene;
        InspecScreenHandler.UpdateScreenStatus += Update_IsInspectonScreensOpen;
        RoomSceneManager.AddItemToInventory += SendAddItem;
    }
    // Sends over the inventory to the UIManager, also passes over the name of the ui that requested it back to it
    public void SendInventoryToUIScene(UIName requestedUI)
    {
        InventoryMainToUI?.Invoke(requestedUI,GetComponent<Inventory>());
    }
    // Gets updated for the current status of UI's 
    public void Update_AreUIOpen(bool givenBool, UIName openedUI){
        areUIOpen = givenBool;
        CurrentUIOpened = openedUI;
    }
    public void Update_IsInspectonScreensOpen(bool status)
    {
        IsInspectonScreensOpen = status;
    }
    public void SendAddItem(Item givenItem)
    {
        GetComponent<Inventory>().AddItem(givenItem);
    }
}
