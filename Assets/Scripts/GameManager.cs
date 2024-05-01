using HeneGames.DialogueSystem;
using Inventory;
using Inventory.Model;
using Inventory.UI;
using PC;
using PC.Model;
using Shop;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Inventory.Model.InventorySO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField]
    private PCPlayerController PC;

    [SerializeField]
    private InventoryController IC;
    [SerializeField]
    private ShopController IS;

    [SerializeField]
    private LeanTweenAnimate LTA;


    [SerializeField]
    public Canvas ShopScene;

    [SerializeField]
    public Canvas BuildScene;

    [SerializeField]
    public Canvas HomeScene;

    [SerializeField]
    public Canvas MainMenu;

    [SerializeField]
    public Canvas settingsMenu;

    [SerializeField]
    public Canvas TopUI;

    [SerializeField]
    public Canvas RenamePC;

    // Create a list to store items that need to be transferred to another scene.
    public event Action <InventoryItem> OnItemsToTransferUpdated;
    public List<InventoryItem> itemsToTransfer = new List<InventoryItem>();
    public int tempindex;

    public bool WiresSceneEnabled = false;
    public Dictionary<string, InventoryItem> PSUImagesNeeds = new Dictionary<string, InventoryItem>();
    public double PCMoney = 0;
    public TMP_Text PCMoneyText;
    public TMP_Text ShopPCMoneyText;


    [SerializeField]
    public Image DialogBox;
    [SerializeField]
    public TMP_Text DialogText;

    [SerializeField]
    public TMP_Text playerlabel;
    [SerializeField]
    public TMP_Text BottomplayerName;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private PCInventSO PCData;
    [SerializeField]
    private ShopSO shopData;
    public GameObject DialogUI, Dialog;
    
    public int shopSize = 8;
    public int currentLevel = 1;
    public int experience = 0;
    public int maxExperienceForNextLevel = 20;
    public string PlayerName = "Player";


    public ShopItem Item1;
    public ShopItem Item2;

    public bool BeenModified = false;

    //public void ShowPopUp()
    //{
    //    LTA.shopPopUP();
    //}


    [SerializeField] private GameObject shoppopupPrefab;// Reference to the parent transform where the popups will be placed
    [SerializeField] private Transform shopPopUpParent;

    public void HandleNewItem()
    {
        ShopItem item1 = shopData.GetItemAt(GameManager.Instance.shopSize - 2);
        ShopItem item2 = shopData.GetItemAt(GameManager.Instance.shopSize - 1);

        GameManager.Instance.Item1 = item1;
        GameManager.Instance.Item2 = item2;

    }
    public void SaveData()
    {

        PlayerPrefs.SetInt("ShopSize", shopSize);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("Experience", experience);
        PlayerPrefs.SetInt("MaxExperienceForNextLevel", maxExperienceForNextLevel);
        PlayerPrefs.SetString("PlayerName", PlayerName);

        // Save PlayerPrefs to disk
        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        shopSize = PlayerPrefs.GetInt("ShopSize", 8); // Default value is 8 if it hasn't been set yet
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Default value is 0 if it hasn't been set yet
        experience = PlayerPrefs.GetInt("Experience", 0); // Default value is 0 if it hasn't been set yet
        maxExperienceForNextLevel = PlayerPrefs.GetInt("MaxExperienceForNextLevel", 20);
        PlayerName = PlayerPrefs.GetString("PlayerName", "Player");// Default value is 20 if it hasn't been set yet
    }
    public void CheckTutorialCompletion()
    {
        if (PlayerPrefs.GetInt("TutorialDone") == 1)
        {
           
            DialogUI.SetActive(false);
            Dialog.SetActive(false);
            playerlabel.gameObject.SetActive(true);
            BottomplayerName.gameObject.SetActive(true);
            BottomplayerName.text = PlayerName; 



        }
        else if (PlayerPrefs.GetInt("TutorialDone") == 0 /*|| PlayerPrefs.GetInt("TutorialDone") == null*/)
        {
            PlayerPrefs.DeleteAll();
            DialogUI.SetActive(true);
            Dialog.SetActive(true);
            playerlabel.gameObject.SetActive(false);
            BottomplayerName.gameObject.SetActive(false);
            inventoryData.inventoryItems.Clear();
            PCData.ComputerItems.Clear();
            shopSize = 8;
            currentLevel = 1;
            experience = 0;
            maxExperienceForNextLevel = 20;
            PlayerName = "Player";

        }
    }

    public void ShowPopUp(InventoryItem inventoryItem, double total)
    {
        GameObject newShopPopup = Instantiate(shoppopupPrefab, shopPopUpParent); // Instantiate the popup as a child of the designated parent

        LeanTween.move(newShopPopup, new Vector3(0f, -4f, 0f), 1f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUp(newShopPopup));

        UpdateShopPopup(newShopPopup, inventoryItem, total);
    }

    private void HidePopUp(GameObject shopPopup)
    {
        LeanTween.move(shopPopup, new Vector3(0f, -8f, 0f), 1f)
            .setDelay(1f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => Destroy(shopPopup));
    }

    private void UpdateShopPopup(GameObject shopPopup, InventoryItem inventoryItem, double total)
    {
        Image[] images = shopPopup.GetComponentsInChildren<Image>();
        TMP_Text[] texts = shopPopup.GetComponentsInChildren<TMP_Text>();

        foreach (var image in images)
        {
            // Check conditions or naming conventions to identify the image elements you need to update
            if (image.gameObject.name == "ItemImage") // Assuming the GameObject name is set in the editor
            {
                image.sprite = inventoryItem.item.ItemImage;
                break; // Assuming there's only one image to update
            }
        }

        foreach (var text in texts)
        {
            // Check conditions or naming conventions to identify the text elements you need to update
            if (text.gameObject.name == "Quantity") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.quantity.ToString() + "X";
            }
            else if (text.gameObject.name == "ItemName") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.item.Name;
            }
            else if (text.gameObject.name == "Price") // Assuming the GameObject name is set in the editor
            {
                text.text = "For $" + total.ToString();
            }
        }
    }
    //public InventoryController IC;
    private void Start()
    {
        LoadData();
        CheckTutorialCompletion();
        LoadPCMoney();
        // Load PCMoney when the game starts
                   // PC.LoadInitialItems();
                   //SceneManager.LoadScene("Desk");
                   //SceneManager.LoadScene("Home", LoadSceneMode.Additive);
                   //BuildScene.gameObject.SetActive(false);
                   //ShopScene.gameObject.SetActive(false);
                   //HomeScene.gameObject.SetActive(false);
                   //settingsMenu.gameObject.SetActive(false);
                   //TopUI.gameObject.SetActive(false);
                   //RenamePC.gameObject.SetActive(false);
                   //MainMenu.gameObject.SetActive(true);

    }

    private void Awake()
    {
       
        if (Instance == null)
        {
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            //IC.LoadInitialItems();
            // Keep this object alive between scenes.
        }
        else
        {
            Destroy(gameObject);


        }
    }
    /*private void Start()
    {
        LoadInitialItems();
    }*/
    public void AddItemToTransfer(InventoryItem item)
    {
        itemsToTransfer.Add(item);
        OnItemsToTransferUpdated?.Invoke(item);
    }
    public void SavePCMoney()
    {
        // Convert double value to string before saving
        string moneyAsString = PCMoney.ToString();

        // Save the string representation of the double value using PlayerPrefs
        PlayerPrefs.SetString("PCMoneyKey", moneyAsString);

        // Save PlayerPrefs data to disk
        PlayerPrefs.Save();
    }
    public void UpdatePCMoneyText()
    {
        PCMoneyText.text = "$" + PCMoney.ToString();
        ShopPCMoneyText.text = "$" + PCMoney.ToString();// Update the UI text with PCMoney
    }
    private void LoadPCMoney()
    {
        // Retrieve the stored string value from PlayerPrefs using the specified key
        string moneyAsString = PlayerPrefs.GetString("PCMoneyKey"); // Default value is "0" if key doesn't exist

        // Parse the string back to double
        PCMoney = double.Parse(moneyAsString);
    }


    public void SaveInitialItems(List<InventoryItem> items)
    {
        
         string jsonData = JsonUtility.ToJson(new InventoryItemList { Items = items });
         PlayerPrefs.SetString("SavedInitialItems", jsonData);
         PlayerPrefs.Save();
        //Debug.LogError("Data has been Saved");
    }
    public void SaveComputerItems(List<Computer> PCitems)
    {

        string jsonData = JsonUtility.ToJson(new ComputerItemList { PCItems = PCitems });
        PlayerPrefs.SetString("SavedComputerItems", jsonData);
        PlayerPrefs.Save();
       // Debug.LogError("Data has been Saved");
    }

    public void SaveUniqueIndex(List<int> unique)
    {

        string jsonData = JsonUtility.ToJson(new UniqueIndexes { unique = unique });
        PlayerPrefs.SetString("SavedUniqueIndex", jsonData);
        PlayerPrefs.Save();
        // Debug.LogError("Data has been Saved");
    }

    public void BackSingleItem(string category)
    {
        IC.HandleBackItem(category);
    }

    /* public void LoadInitialItems()
     {
         string savedData = PlayerPrefs.GetString("SavedInitialItems");
         InventoryItemList loadedData = JsonUtility.FromJson<InventoryItemList>(savedData);
         //Debug.LogError(loadedData);
         if (loadedData != null)
         {
             foreach (InventoryItem item in loadedData.Items)
             {

                 itemsToTransfer.Add(item);
                 //inventoryData.AddItem(item);

             }
             //initialItems.AddRange(loadedData.Items);
             Debug.LogWarning("Data has been Loaded");
       }

     }*/
    private void Update()
    {
      
        if (PlayerPrefs.GetInt("TutorialDone") == 1)
        {
            //shopSize = IS.GetUsedSlotsCount();
            LoadData();
            playerlabel.gameObject.SetActive(true);
            BottomplayerName.gameObject.SetActive(true);
            BottomplayerName.text = PlayerName;
            //ordsBTN.interactable = true;
        }
        //CheckTutorialCompletion();
        //inventoryData.SaveItems();
    }
    private void OnApplicationQuit()
    {
        
    }
}
[System.Serializable]
public class InventoryItemList
{
    public List<InventoryItem> Items;
}
[System.Serializable]
public class ComputerItemList
{
    public List<Computer> PCItems;
}

[System.Serializable]
public class UniqueIndexes
{
    public List<int> unique;
}




