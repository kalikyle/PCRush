using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Shop.UI;
using System.Linq;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ShopSO : ScriptableObject//how many of the items
    {
        
        [SerializeField]
        public List<ShopItem> ShopItems;

        public ShopItem GetItemAt(int obj)
        {
            return ShopItems[obj];
        }
        
        public Dictionary<int, ShopItem> GetCurrentInventoryState()
        {
            Dictionary<int, ShopItem> returnValue = new Dictionary<int, ShopItem>();
            for (int i = 0; i < ShopItems.Count; i++)
            {
                if (ShopItems[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = ShopItems[i];
            }
            return returnValue;
        }
        //this is for the filtering, getting the category of the item
        public List<ShopItem> GetItemsByCategory(string category)
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Category.Equals(category)).ToList();
        }

    }


    [Serializable]
    public struct ShopItem
    {

        public ItemSO item;
        public bool isEmpty => item == null;

        public static ShopItem GetEmptyItem() => new ShopItem
        {
            item = null,
        };
    }
}
