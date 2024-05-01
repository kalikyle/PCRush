using Inventory;
using Inventory.Model;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Inventory.Model.InventorySO;

namespace Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField]
        private ShopPage shoP;
        [SerializeField]
        private ShopSO shopData;

        [SerializeField]
        private NumericUpDown NumUpDown;

        [SerializeField]
        private Canvas ShopScene;

        [SerializeField]
        private Canvas TopUI;

        [SerializeField]
        private TMP_Dropdown CategoryDropdown;




        /* [SerializeField]
         private Button cpuButton;
        
         [SerializeField]
         private Button motherboardButton;
         [SerializeField]
         private Button ramButton;
         [SerializeField]
         private Button othersButton;
         [SerializeField]
         private Button caseButton;
         [SerializeField]
         private Button psuButton;
         [SerializeField]
         private Button gpuButton;
         [SerializeField]
         private Button storageButton;*/








        public event Action<int> OnDescriptionRequested;

        private void Start()
        {

            PrepareUI();//initial size of the shop\
                        //shopData.Initialize();
            NumUpDown.ToggleTF = false;
            ShowAllCategory();
            shoP.Show();

        }
        //also create new list for filtered items
        
        public Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();
        private List<Inventory.Model.ShopItem> itemsShownInAllCategory = new List<Inventory.Model.ShopItem>();

        private void ShowAllCategory()
        {
            NumUpDown.ToggleTF = false;
            itemsShownInAllCategory.Clear();

            var nonEmptyItems = shopData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

            int displayedItemsCount = 0;
            foreach (var item in nonEmptyItems)
            {
                if (displayedItemsCount >= GameManager.Instance.shopSize)
                    break;

                shoP.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.item.Name, item.Value.item.Price.ToString(), item.Value.item.Category);
                itemsShownInAllCategory.Add(item.Value); // Add to items shown in "All" category
                displayedItemsCount++;
            }
        }

        //private void ShowAllCategory()//this to show all
        //{
        //    NumUpDown.ToggleTF = false;
        //    foreach (var item in shopData.GetCurrentInventoryState()) //show all
        //    {
        //        shoP.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.item.Name, item.Value.item.Price.ToString(), item.Value.item.Category);

        //    }

        //}
        public int GetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in shopData.ShopItems)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }
       
        private void PrepareUI()
        {
            shoP.InitializedShop(/*GetUsedSlotsCount()*/ GameManager.Instance.shopSize);
            shoP.OnDescriptionRequested += HandleDescriptionRequest;
            OnDescriptionRequested += HandleDescriptionRequests;//create new event handler
            //shoP.OnItemActionRequested += HandleItemActionRequest;


            //filtering, then here is the button if clicked, EXAMPLE FOR BUTTON
            // cpuButton.onClick.AddListener(() => ShowCategory("CPU"));
            /*
             caseButton.onClick.AddListener(() => ShowCategory("Case"));
             motherboardButton.onClick.AddListener(() => ShowCategory("Motherboard"));
             ramButton.onClick.AddListener(() => ShowCategory("RAM"));
             psuButton.onClick.AddListener(() => ShowCategory("PSU"));
             storageButton.onClick.AddListener(() => ShowCategory("Storage"));
             gpuButton.onClick.AddListener(() => ShowCategory("Video Card"));
             othersButton.onClick.AddListener(() => ShowCategory("Others"));


             //all
             allButton.onClick.AddListener(() => shoP.ResetSelection());
             allButton.onClick.AddListener(() => shoP.ClearItems());
             allButton.onClick.AddListener(() => ToggleALLButton());*/

            

        }
        // FOR DROPDOWN
     
       public void HandleCategory(int val)
        {
            
            switch (val){
                case 0://all
                    ToggleALLButton();
                    break;
                case 1:
                    ShowCategory("Case");
                    
                    break;
                case 2:
                    ShowCategory("Motherboard");
                    
                    break;
                case 3:
                    ShowCategory("CPU");

                    break;
                case 4:
                    ShowCategory("CPU Fan");

                    break;
                case 5:
                    ShowCategory("RAM");
                    
                    break;
                case 6:
                    ShowCategory("Video Card");
                    
                    break;
                case 7:
                    ShowCategory("Storage");
                    break;
                case 8:
                    ShowCategory("PSU");
                    break;
            }
        }

      

        public void ToggleALLButton()
        {
           NumUpDown.ToggleTF = false;
            // Toggle the state
           shoP.ResetSelection();
           shoP.ClearItems();
           shoP.InitializedShop(/*GetUsedSlotsCount()*/  GameManager.Instance.shopSize);

           CategoryDropdown.value = 0;

           ShowAllCategory();
               
        }
        private string currentCategory = "";


        //now create a method to show the category
        public List<Inventory.Model.ShopItem> itemsToShow;
        //private void ShowCategory(string category)
        //{
        //  NumUpDown.ToggleTF = true;
        //    currentCategory = category;
        //    shoP.ResetSelection();
        //    NumUpDown.filteredItems.Clear();
        //    tempToOriginalIndexMapping.Clear();
        //    shoP.ClearItems();// Clear the existing items in the UI

        //    itemsToShow = shopData.GetItemsByCategory(currentCategory);//all by category

        //    int originalIndex = 0;
        //   int tempIndex = 0;

        //   foreach (var item in itemsToShow)//loop each categorized item
        //    {
        //        // Add items to the filtered list and store the mapping

        //        NumUpDown.filteredItems.Add(item);//then add to filteredItems

        //       tempToOriginalIndexMapping[tempIndex] = originalIndex;

        //       //create a new filteredItems
        //       shoP.AddShopItem(item.item.ItemImage, item.item.Name, item.item.Price.ToString(), item.item.Category);
        //       originalIndex++;
        //        tempIndex++;
        //    }

        //}
        private void ShowCategory(string category)
        {
            NumUpDown.ToggleTF = true;
            currentCategory = category;
            shoP.ResetSelection();
            NumUpDown.filteredItems.Clear();
            tempToOriginalIndexMapping.Clear();
            shoP.ClearItems();// Clear the existing items in the UI

            itemsToShow = shopData.GetItemsByCategory(currentCategory);//all by category

            int displayedItemsCount = 0;
            foreach (var item in itemsToShow)
            {
                if (displayedItemsCount >= GameManager.Instance.shopSize)
                    break;

                if (itemsShownInAllCategory.Contains(item)) // Check if the item is in the "All" category
                {
                    NumUpDown.filteredItems.Add(item);//then add to filteredItems
                    tempToOriginalIndexMapping[displayedItemsCount] = displayedItemsCount;

                    shoP.AddShopItem(item.item.ItemImage, item.item.Name, item.item.Price.ToString(), item.item.Category);
                    displayedItemsCount++;
                }
            }
        }



        private void HandleDescriptionRequest(int obj)
        {
           
            Inventory.Model.ShopItem shopItems = shopData.GetItemAt(obj);
            if (shopItems.isEmpty) {

                shoP.ResetSelection();
                return; 
            
            }
            ItemSO item = shopItems.item;
            shoP.UpdateDescription(obj, item.ItemImage, item.Name, item.Category, item.Price, item.Description);
        }

        //this handler is for filtered items
        public void HandleDescriptionRequests(int obj)
        {
            if (obj >= 0 && obj < NumUpDown.filteredItems.Count)
            {
                Inventory.Model.ShopItem shopItem = NumUpDown.filteredItems[obj];
                if (!shopItem.isEmpty)
                {
                    ItemSO item = shopItem.item;
                    shoP.UpdateDescription(obj, item.ItemImage, item.Name, item.Category, item.Price, item.Description);
                }
            }
        }

        private void Update()
        {
            //shoP.Show();

            //if (GameManager.Instance.PCMoney < GameManager.Instance.total) 
            //{
            //   DialogBox.gameObject.SetActive(true);
            //    DialogText.text = "Insufficient PCMoney! \n Sell Some Parts to Restore your PCMoney...";
            //}
        }
        //handle for item if clicked
        public void HandleItemSelection(int tempIndex)
        {
            // Use the mapping to get the original index
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
            {
                OnDescriptionRequested?.Invoke(originalIndex);
                Debug.Log("Temporary Index: " + tempIndex);
                Debug.Log("Original Index: " + originalIndex);
            }
        }

       

      




    }


}

