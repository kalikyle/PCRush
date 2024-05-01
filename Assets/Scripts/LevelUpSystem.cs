using Orders.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSystem : MonoBehaviour
{
    public Slider levelSlider;
    public TMP_Text CurrentLevel;
    public TMP_Text NextLevel;

    public TMP_Text Exper;

    public Canvas LevelUP;
    public TMP_Text newLevel;
    public Image NewItem1;
    public Image NewItem2;
    public TMP_Text newitem1Name;
    public TMP_Text newitem2Name;
    public TMP_Text PLayerName;

    public LeanTweenAnimate LTA;

    public MissionConSO MissionData;


    int nextlevel;


    public void Start()
    {
        CurrentLevel.text = GameManager.Instance.currentLevel.ToString();
        NextLevel.text = nextlevel.ToString();
        PLayerName.text = GameManager.Instance.PlayerName;
    }
    public void Update()
    {
       
        levelSlider.value = (float)GameManager.Instance.experience / GameManager.Instance.maxExperienceForNextLevel;
        nextlevel = GameManager.Instance.currentLevel + 1;
        NextLevel.text = nextlevel.ToString();
        PLayerName.text = GameManager.Instance.PlayerName;
        Exper.text = GameManager.Instance.experience.ToString() + "/" + GameManager.Instance.maxExperienceForNextLevel.ToString();
       
        // Check if the player has enough experience to level up.

        if (GameManager.Instance.experience >= GameManager.Instance.maxExperienceForNextLevel)
        {
            LevelUp();
            Debug.Log(true);
           
        }
    }
    //void LevelUp()
    //{

    //    GameManager.Instance.currentLevel++;
    //    GameManager.Instance.experience = 0;
    //    GameManager.Instance.shopSize += 2;
    //    GameManager.Instance.maxExperienceForNextLevel += 20;

    //    CurrentLevel.text = GameManager.Instance.currentLevel.ToString();
    //    NextLevel.text = nextlevel.ToString();
    //    GameManager.Instance.SaveData();// Reset experience to start filling the bar again.
    //    // Perform other level up actions, like increasing stats, unlocking abilities, etc.
    //}

    public void LevelUp()
    {
        int excessExperience = GameManager.Instance.experience - GameManager.Instance.maxExperienceForNextLevel;

        if (excessExperience > 0)
        {
            // Carry over excess experience to the next level
            GameManager.Instance.currentLevel++;
            GameManager.Instance.experience = excessExperience;
        }
        else
        {
            GameManager.Instance.currentLevel++;
            GameManager.Instance.experience = 0;
        }

        GameManager.Instance.shopSize += 2;
        GameManager.Instance.maxExperienceForNextLevel += 20;

        CurrentLevel.text = GameManager.Instance.currentLevel.ToString();
        NextLevel.text = (GameManager.Instance.currentLevel + 1).ToString();
        GameManager.Instance.HandleNewItem();
        GameManager.Instance.SaveData();

        

        // LevelUP.gameObject.SetActive(true);
        LTA.showLevelUP();
        newLevel.text = GameManager.Instance.currentLevel.ToString();
        NewItem1.sprite = GameManager.Instance.Item1.item.ItemImage;
        NewItem2.sprite = GameManager.Instance.Item2.item.ItemImage;
        newitem1Name.text = GameManager.Instance.Item1.item.Name;
        newitem2Name.text = GameManager.Instance.Item2.item.Name;
       
       

        


        // Perform other level up actions, like increasing stats, unlocking abilities, etc.
    }
}
