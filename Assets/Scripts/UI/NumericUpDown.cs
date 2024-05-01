using Inventory;
using Inventory.Model;
using Shop;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Inventory.Model.InventorySO;

public class NumericUpDown : MonoBehaviour
{
    public TMP_Text displayText;
    public TMP_Text priceText;
    public Button incrementButton;
    public Button decrementButton;
    public Button buyButton;

    private List<Shop.UI.ShopItem> selectedItems = new List<Shop.UI.ShopItem>();
    private List<Shop.UI.ShopItem> toBuy= new List<Shop.UI.ShopItem>();
    public List<Inventory.Model.ShopItem> filteredItems;

    public AudioSource buySound;
    public AudioSource InsufficientSound;




    public Inventory.Model.ShopSO so;

    


    public bool ToggleTF = false;
    private int value = 1;
    public double ItemPrice = 0;
    private int minValue = 1; // Minimum value (decrement limit)
    private int maxValue = 99;// Maximum value (increment limit)
    double total;
    

    private void Start()
    {
        
        // Attach click listeners to the buttons
        incrementButton.onClick.AddListener(IncrementValue);
        decrementButton.onClick.AddListener(DecrementValue);
        buyButton.onClick.AddListener(HandleThePurchase);
        // Initialize the display

        UpdateDisplay();
        DisableBuyButton();

    }

    private void IncrementValue()
    {
        if (value < maxValue)
        {
            value++;
            UpdateDisplay();
            UpdatePriceDisplay();


        }
    }

    private void DecrementValue()
    {
        if (value > minValue)
        {
            value--;
            UpdateDisplay();
            UpdatePriceDisplay();


        }
    }
    
    private void UpdateDisplay()
    {
       
        if (displayText != null)
        {
            displayText.text = value.ToString();
            
        }
       
    }
    private void UpdatePriceDisplay()
    {
        total = ItemPrice * value; 
        if (priceText != null)
        {
            priceText.text = "$" + total.ToString();

        }

    }

    public void SelectItem(Shop.UI.ShopItem item)
    {

        if (!selectedItems.Contains(item))// selectedItem = 0 item + 1
        {
            // DeselectItem(item);
            SelectNewOrDeselectPrevious(item);

        }
        else
        {
            toBuy.Clear();
            SelectNewOrDeselectPrevious(item);
        }
        
    }
    private void SelectNewOrDeselectPrevious(Shop.UI.ShopItem item)
    {
        Debug.Log("Item Called to Buy.");
        item.select();

        selectedItems.Add(item);
        toBuy.Add(item);


        if (ToggleTF == true)
        {
            int tempIndex;
            tempIndex = item.temporaryIndex;
            Inventory.Model.ShopItem sp = filteredItems[tempIndex];
            ItemPrice = sp.item.Price;
            total = sp.item.Price;
        }
        else
        {
            int index = item.temporaryIndex;
            Inventory.Model.ShopItem sp = GetItemAt(index);
            ItemPrice = sp.item.Price;
            total = sp.item.Price;
        }


           


        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        UpdateBuyButtonInteractability();






    }

    private void HandleThePurchase ()
    {
          if (toBuy.Count > 0)
           {
            Shop.UI.ShopItem shopItem = toBuy[0];
               if (shopItem != null)
               {
                //you can place the condition for currency here
                HandlePurchase(shopItem);
                //ConvertShopItemToInventoryItem(shopItem);
                Debug.Log("The item has been purchase");
                Debug.Log("The item is " + ConvertShopItemToInventoryItem(shopItem).item.Name); 
                Debug.Log("Quantity: " + ConvertShopItemToInventoryItem(shopItem).quantity);
                shopItem.DeSelect();
                value = 1;
                displayText.text = value.ToString();
                priceText.text = "$"+ConvertShopItemToInventoryItem(shopItem).item.Price.ToString();


            }
            else
               {
                   Debug.LogWarning("Null shop item in toBuy list.");
               }
           }
           else
           {
               Debug.Log("No item to purchase. The toBuy list is empty.");
           }
        toBuy.Clear();
           
    }


    private void UpdateBuyButtonInteractability()
    {
        //Debug.Log("UpdateBuyButtonInteractability called.");
        //Debug.Log("Selected Items: " + string.Join(", ", selectedItems));
        buyButton.interactable = (selectedItems.Count > 0);
    }
    private void DisableBuyButton()
    {
        buyButton.interactable = false;

    }
    public Inventory.Model.ShopItem GetItemAt(int obj)// this will return the ShopItem SO
    {
        return so.ShopItems[obj];
    }
    private Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();

    //Inventory.Model.ItemSO item;
    public InventoryItem ConvertShopItemToInventoryItem(Shop.UI.ShopItem shopItem)
    {
      
        List<Inventory.Model.ShopItem> shopItems = so.ShopItems;
        InventoryItem inventoryItem = new InventoryItem();

        int tempIndexs;
        int originalIndex;
        int tempIndex;
        if (ToggleTF == true)
        {
            Debug.Log("Toggle: True");
            Debug.Log(filteredItems.Count);
            tempIndex = shopItem.temporaryIndex;
            Inventory.Model.ShopItem shpItem = filteredItems[tempIndex];

            if (!shpItem.isEmpty)
            {
                inventoryItem.item = shpItem.item;
            }
            else
            {
                Debug.Log("ShopItem is empty");
            }
        }
        else
        {
            tempToOriginalIndexMapping.Clear();
            tempIndexs = 0;
            originalIndex = 0;
            Debug.Log("Toggle: False");
            //Debug.Log(shopItems.Count);
            foreach (var item in shopItems)
            {
                
                tempToOriginalIndexMapping[tempIndexs] = originalIndex;
                tempIndexs++;
                originalIndex++;
               
            }
            
            tempIndex = shopItem.temporaryIndex;
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
            {
                // Use the original index to retrieve the ShopItem
                Inventory.Model.ShopItem shpItem = GetItemAt(originalIndexs);

                if (!shpItem.isEmpty)
                {
                    inventoryItem.item = shpItem.item;
                }
                else
                {
                    Debug.Log("ShopItem is empty");
                }
            }
            else
            {
                Debug.LogError("Mapping not found for temporary index: " + tempIndex);
            }
        }
       
      
        // Use the methods in the ShopItem class to retrieve the ItemSO and quantity.
        inventoryItem.quantity = Convert.ToInt32(value.ToString());
        
        
        // You can set other properties as needed
        return inventoryItem;

    }

    
    //public List<InventoryItem> AddedtoCart = new List<InventoryItem>();//for cart sana
    public void HandlePurchase(Shop.UI.ShopItem shopItem)//need fix
    {
        if (shopItem != null)
        {
            
                InventoryItem inventoryItem = ConvertShopItemToInventoryItem(shopItem);
                //GameManager.Instance.SaveInventoryItem(inventoryItem);

                if (inventoryItem.isEmpty)
                {
                    // Add the converted item to the inventory's initialItems list
                    //inventoryController.itemsToTransfer.Add(inventoryItem);
                    Debug.LogWarning("Null inventory item returned from conversion.");
                }
                else
                {
                //place the if else here for currency
               /* GameManager dataManager = FindObjectOfType<GameManager>();
                dataManager.AddItemToTransfer(inventoryItem);*/
                if  (GameManager.Instance.PCMoney >= total)
                {
                    
                    GameManager.Instance.PCMoney -= total;
                    GameManager.Instance.AddItemToTransfer(inventoryItem);
                    GameManager.Instance.UpdatePCMoneyText();
                    GameManager.Instance.SavePCMoney(); // Save the updated PCMoney



                    //GameManager.Instance.PopImage.gameObject.SetActive(true);
                    GameManager.Instance.ShowPopUp(inventoryItem,total);
                    buySound.Play();
                    //GameManager.Instance.PopupItemImage.sprite = inventoryItem.item.ItemImage;
                    //GameManager.Instance.Quantity.text = inventoryItem.quantity.ToString() + "X";
                    //GameManager.Instance.ItemName.text = inventoryItem.item.Name;
                    //GameManager.Instance.Price.text = "For $" + total.ToString();

                                                        // Update the PCMoney text UI
                }
                else
                {
                    GameManager.Instance.DialogBox.gameObject.SetActive(true);
                    GameManager.Instance.DialogText.text = "Insufficient PCMoney! \n Sell Some Parts to Restore your PCMoney...";
                    InsufficientSound.Play();
                }

                // AddedtoCart.Add(inventoryItem);





                //inventdata.inventoryItems.Add(inventoryItem);

                Debug.Log("Item added to inventory ");
                }
            
        }
        else
        {
            Debug.LogWarning("Null shop item passed to HandlePurchase.");
        }

    }

    public void Update()
    {
        if (PlayerPrefs.GetInt("TutorialDone") == 1)
        {


           incrementButton.gameObject.SetActive(true);
           decrementButton.gameObject.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("TutorialDone") == 0 /*|| PlayerPrefs.GetInt("TutorialDone") == null*/)
        {
            incrementButton.gameObject.SetActive(false);
            decrementButton.gameObject.SetActive(false);

        }
    }



}