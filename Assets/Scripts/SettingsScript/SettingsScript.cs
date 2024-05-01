using Inventory.Model;
using PC.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.Image DialogBox;
    [SerializeField]
    public TMP_Text DialogText;
    [SerializeField]
    public UnityEngine.UI.Button NoButton;
    [SerializeField]
    public UnityEngine.UI.Button YesButton;

    public Quit quit;

    [SerializeField]
    private InventorySO inventoryData;

   

    [SerializeField]
    private PCInventSO PCData;

   




    // Start is called before the first frame update
    void Start()
    {
        YesButton.onClick.AddListener(Yes);
        NoButton.onClick.AddListener(No);

      

    }
    
    public void ClearPlayerPrefData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs have been cleared.");
    }

    public void PrompTDelete()
    {
        DialogBox.gameObject.SetActive(true);
        DialogText.text = "Do you want to <color=#FF0000>Delete all your Data</color>? \n All Of your Saved Item, Progression, and Computers will be Deleted... \n Also you Need to Restart the Game";
    }
    public void No()
    {
        DialogBox.gameObject.SetActive(false);
    }
    public void Yes()
    {
        inventoryData.inventoryItems.Clear();
        PCData.ComputerItems.Clear();
        ClearPlayerPrefData();
        quit.QuitGame();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
