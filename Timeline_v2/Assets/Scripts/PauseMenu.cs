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
    public bool hasMap = false;
    public bool hasBag = false;
    public bool IsOpen { get; private set; }
    private string currentMenu = "default";
    // Start is called before the first frame update
    void Start()
    {
        //resume, map, inventory, save, quit
        string [] boxNames={"Resume","Map","Inventory","Save","Quit"};
        for (int i=0; i<boxNames.Length; i++)
        {
            GameObject optionSlot=Instantiate(optionBoxTemplate.gameObject,optionBoxContainer.transform);
            optionSlot.gameObject.SetActive(true);
            optionSlot.GetComponentInChildren<TMP_Text>().text=boxNames[i];
            optionSlot.GetComponentInChildren<Button>().onClick.AddListener( () => OnPickedResponse(optionSlot.GetComponentInChildren<TMP_Text>().text));
            // Grey-out map/inventory if the player hasn't picked up the map/bag yet
            if ((boxNames[i].Equals("Inventory") && !hasBag) || (boxNames[i].Equals("Map") && !hasMap)){
                optionSlot.GetComponentInChildren<TMP_Text>().faceColor = new Color32(150,150,150,255);
            }
            closePauseMenu();
        }
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

                case "Save":
                // Save the game
                    break;
                case "Quit":
                // Open "Do you want to quit" box
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
    }
}