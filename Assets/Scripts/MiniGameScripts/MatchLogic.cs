using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchLogic : MonoBehaviour
{
    public static MatchLogic instance;

    public int maxPoints = 4;
    public GameObject PowerSupplyGame;
    private int points = 0;

    public Image Image1;
    public Image Image2;
    public Image Image3;
    public Image Image4;

    public Image psuImage5;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;

    public TMP_Text psutext5;

    public Button CancelBTN;

    //public Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();
    void Start()
    {
        instance = this;
     
        //// Example: Adding items to the dictionary
        //Item cpuItem = new Item { itemName = "CPU", itemImage = GameManager.Instance.PSUImagesNeeds["CPU"].item.ItemImage };
        //Item MBItem = new Item { itemName = "Motherboard", itemImage = GameManager.Instance.PSUImagesNeeds["Motherboard"].item.ItemImage };
        //Item GPUItem = new Item { itemName = "Video Card", itemImage = GameManager.Instance.PSUImagesNeeds["Video Card"].item.ItemImage };
        //Item strgItem = new Item { itemName = "Storage", itemImage = GameManager.Instance.PSUImagesNeeds["Storage"].item.ItemImage };
        //// Add more items...

        //itemDictionary.Add(cpuItem.itemName, cpuItem);
        //itemDictionary.Add(MBItem.itemName, MBItem);
        //itemDictionary.Add(GPUItem.itemName, GPUItem);
        //itemDictionary.Add(strgItem.itemName, strgItem);


       // ShuffleDictionary(itemDictionary);

        Image1.sprite = GameManager.Instance.PSUImagesNeeds["Motherboard"].item.ItemImage;
        Image2.sprite = GameManager.Instance.PSUImagesNeeds["CPU"].item.ItemImage;
        Image3.sprite = GameManager.Instance.PSUImagesNeeds["Storage"].item.ItemImage;
        Image4.sprite = GameManager.Instance.PSUImagesNeeds["Video Card"].item.ItemImage;

        psuImage5.sprite = GameManager.Instance.PSUImagesNeeds["PSU"].item.ItemImage;

        text1.text = GameManager.Instance.PSUImagesNeeds["Motherboard"].item.Name;
        text2.text = GameManager.Instance.PSUImagesNeeds["CPU"].item.Name;
        text3.text = GameManager.Instance.PSUImagesNeeds["Storage"].item.Name;
        text4.text = GameManager.Instance.PSUImagesNeeds["Video Card"].item.Name;

        psutext5.text = GameManager.Instance.PSUImagesNeeds["PSU"].item.Name;

        CancelBTN.onClick.AddListener(() => CancelButton());
    }

    void UpdatePoints()
    {
        if(points == maxPoints)
        {
            //PowerSupplyGame.SetActive(false);
            SceneManager.UnloadSceneAsync("PSUWiring");
            instance.points = 0;
            GameManager.Instance.WiresSceneEnabled = false;
        }
    }
    public void Awake()
    {
        GameManager.Instance.WiresSceneEnabled = true;
    }

    public void CancelButton() {

        SceneManager.UnloadSceneAsync("PSUWiring");
        GameManager.Instance.BackSingleItem("PSU");
    }

    public static void AddPoint()
    {
        AddPoints(1);
    }

    private static void AddPoints(int points)
    {
        instance.points += points;
        instance.UpdatePoints();
    }
    void ShuffleDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>(dictionary);

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            KeyValuePair<TKey, TValue> pair = list[k];
            list[k] = list[n];
            list[n] = pair;
        }

        dictionary.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in list)
        {
            dictionary.Add(pair.Key, pair.Value);
        }
    }

    // Start is called before the first frame update
}
[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemImage;
    // Other properties as needed
}
