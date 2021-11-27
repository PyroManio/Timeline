using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionBoxTemplate;
    //private GameObject[] optionBoxList;
    [SerializeField] private GameObject optionBoxContainer;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private DialogueActivator talkDialogue;
    [SerializeField] private OptionScreen optionScreen;  
    [SerializeField] private MapScreen mapScreen;
    public bool hasMap = false;
    public bool hasBag = false;
    public bool IsOpen { get; private set; }
    private string currentMenu = "default";
    [HideInInspector] private List<GameObject> menuOptionList = new List<GameObject>();
    // Start is called before the first frame update
    public void BagGotten() { 
        hasBag = true; 
        menuOptionList[2].GetComponentInChildren<TMP_Text>().faceColor = new Color32(0,0,0,255);
        }
    public void MapGotten() { 
        hasMap = true; 
        menuOptionList[1].GetComponentInChildren<TMP_Text>().faceColor = new Color32(0,0,0,255);
        }
    void Start()
    {
        //resume, map, inventory, save, quit
        string [] boxNames={"Resume","Map","Inventory","Talk","Options"};
        for (int i=0; i<boxNames.Length; i++)
        {
            GameObject optionSlot=Instantiate(optionBoxTemplate.gameObject,optionBoxContainer.transform);
            optionSlot.gameObject.SetActive(true);
            optionSlot.GetComponentInChildren<TMP_Text>().text=boxNames[i];
            optionSlot.GetComponentInChildren<Button>().onClick.AddListener( () => OnPickedResponse(optionSlot.GetComponentInChildren<TMP_Text>().text));
            // Grey-out map/inventory if the player hasn't picked up the map/bag yet
            if ((boxNames[i].Equals("Inventory") && !hasBag) || (boxNames[i].Equals("Map") && !hasMap)){
                //optionSlot.GetComponentInChildren<TMP_Text>().faceColor = new Color32(150,150,150,255);
                optionSlot.GetComponentInChildren<TMP_Text>().color = new Color32(150,150,150,255);
            }
            menuOptionList.Add(optionSlot);
            
        }
        closePauseMenu();
    }
    private void OnPickedResponse(string option)
        {
            switch (option)
            {
                case "Resume":
                // Close out of menu
                    closePauseMenu();
                    break;

                case "Map":
                    if (hasMap)
                    {
                        currentMenu="map";
                        hidePauseMenu();
                        mapScreen.OpenScreen();
                        // Open Map 
                    }
                    break;

                case "Inventory":
                    if (hasBag)
                    {
                        currentMenu="inventory";
                        hidePauseMenu();
                        inventoryUI.openInventory();
                        // Open Inventory
                    }
                    break;

                case "Talk":
                // Save the game
                    // I lied, there is no saving
                    talkDialogue.Interact(GetComponentInChildren<DialogueUI>());
                    closePauseMenu();
                    break;
                case "Options":
                // Open "Do you want to quit" box
                //Jk, no
                    currentMenu = "options";
                    optionScreen.openScreen();
                    hidePauseMenu();
                    break;
                default:
                    Debug.Log("WARNING: Invalid Option Selected");
                    break;
            }
        }
    public void closePauseMenu()
    {
        hidePauseMenu();
        IsOpen = false;
    }
    private void hidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void openPauseMenu()
    {
        currentMenu = "default";
        pauseMenu.SetActive(true);
        IsOpen = true;
    }
    void Update()
    {
        if (currentMenu.Equals("inventory") && !inventoryUI.IsOpen) 
        {
            openPauseMenu();
        }
        if (currentMenu.Equals("options") && !optionScreen.IsOpen) openPauseMenu();
        if (currentMenu.Equals("map") && !mapScreen.IsOpen) openPauseMenu();
    }
}
