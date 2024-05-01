using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.UI
{
    public class ShopPage : MonoBehaviour
    {
        [SerializeField]
        private ShopItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private ShopDesc shopDesc;

        public ShopController shopC;

        public NumericUpDown NumericUpDown;

        public List<ShopItem> ListOfShopItems = new List<ShopItem>();

        public event Action<int> OnDescriptionRequested;

       

        /* public Sprite Image;
         public string title, description, price, category;*///served as temporary data

        public void Start()//from the start this statement whill be executed
        {
            //ListOfShopItems[0].SetData(Image,title, price, category);//gumagana//adding temporary to the list
            // Attach the click listener to the BuyButton
            
            // Set the initial state of the Buy button
          
        }
        private void Awake()
        {
            shopDesc.Hide();
            shopDesc.ResetDescription();
        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                ShopItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                ListOfShopItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
               
            }
        }
        private void DeselectAllItems()
        {
            foreach (ShopItem item in ListOfShopItems)
            {
                item.DeSelect();
            }
        }
        internal void ResetSelection()
        {
            shopDesc.Hide();
            shopDesc.ResetDescription();
            DeselectAllItems();
        }

        internal void UpdateDescription(int obj, Sprite itemImage, string name, string category, double price, string description)
        {
            shopDesc.Show();
            shopDesc.SetDescription(itemImage, name, description, category, price.ToString());
            DeselectAllItems();
            ListOfShopItems[obj].select();

        }

        public void UpdateData(int itemIndex, Sprite ItemImage, string Itemtitle, string Itemprice, string Itemcategory)
        {
            if (ListOfShopItems.Count > itemIndex)
            {
                ListOfShopItems[itemIndex].SetData(ItemImage, Itemtitle, "$" + Itemprice, Itemcategory);//this will add to the shop
               
            }
        }
        private void HandleShowItemActions(ShopItem obj)//if right clicked
        {
            
        }

        private void HandleItemSelection(ShopItem obj)//if clicked
        {
            //shopDesc.SetDescription(Image, title, description, category, price);
            //ListOfShopItems[0].select();
           
                int index = ListOfShopItems.IndexOf(obj);
                if (index == -1)
                {
                    return;
                }
            OnDescriptionRequested?.Invoke(index);
            
        }
        

        public void Show()//show are looping in the controller using update method
        {
            gameObject.SetActive(true);
            
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void ClearItems()
        {
            foreach (var item in ListOfShopItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfShopItems.Clear();
        }


       //then this for adding a new filtered items
        public void AddShopItem(Sprite itemImage, string itemName, string price, string category)
        {
           
            ShopItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            ListOfShopItems.Add(uiItem);//add shop items in the list
            uiItem.SetData(itemImage, itemName, "$" + price, category);
            int itemIndex = NumericUpDown.filteredItems.Count - 1;//this is for the filtered items
            Debug.Log(NumericUpDown.filteredItems.Count);
            uiItem.SetTemporaryIndex(itemIndex);

            uiItem.OnItemClickeds += (tempIndex) => {

                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (shopC != null)
                {
                    shopC.HandleItemSelection(tempIndex);
                    
                }
                else
                {
                    Debug.Log("shopC is not assigned.");
                }
            };
           
            /*uiItem.OnItemClicked += (ShopItem) =>
            {
                HandleItemSelection(ShopItem);
                // Your code here, using the 'id' parameter
                // This lambda expression takes the 'id' as an int parameter
            };*/


            //need fix
        }
        
    }
}
