using HeneGames.DialogueSystem;
using PC.Model;
using System.Collections;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class DialogueTutorial : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Sprite player;
    public string narratorName;
    UnityEvent defaultEvent = new UnityEvent();
    public LeanTweenAnimate LTA;

    private bool OnPlayClickExecuted = false;
    private bool OnOrdersClickExecuted = false;
    private bool OnOrdersHideClickExecuted = false;
    private bool OnInventoryClickExecuted = false;
    private bool OnBuildRoomClickExecuted = false;
    private bool OnInventoryClickAgainExecuted = false;
    private bool OnInventoryClickCloseExecuted = false;
    private bool OnUseClickExecuted = false;
    private bool OnShopClickExecuted = false;
    private bool OnShopCloseExecuted = false;
    private bool OnThermalExecuted = false;
    private bool OnDoneThermalClickExecuted = false;
    private bool OnWiresExecuted = false;
    private bool OnWiresClose = true;
    private bool OnCheckInRenameClickExecuted = false;
    private bool OnDoneBuildPCExecuted = true;
    private bool OnTestPCExecuted = false;
    private bool OnTurnOnExecuted = false;
    private bool OnTurnOffExecuted = false;
    private bool OnComputerInvExecuted = false;
    private bool OnComputerCloseExecuted = false;
    private bool OnOrdersOpenExecuted = true;
    private bool OnSubmitExecuted = false;
    private bool OnEndExecuted= false;

    public Button shopButton, BuildButton, BuildBackButton, OrdersButton, ComputerButton, inventoryButton, ShopinBuildButton, CaseButton,useBTN,SellBTN, BackHomeBTN,invexitButton, testPCBTN, ModifyPCBtn, MainMenuBTN, successCloseBTN,returnBTN;
    public Canvas thermalCanvas;
    

    bool ps = false;
    bool orBtn = false;
    bool XBtn = true;
    public void CheckTutorial()
    {
        bool allExecuted = OnPlayClickExecuted && OnOrdersClickExecuted && OnOrdersHideClickExecuted &&
                       OnInventoryClickExecuted && OnBuildRoomClickExecuted && OnInventoryClickAgainExecuted &&
                       OnInventoryClickCloseExecuted && OnUseClickExecuted && OnShopClickExecuted &&
                       OnShopCloseExecuted && OnThermalExecuted && OnDoneThermalClickExecuted &&
                       OnWiresExecuted && OnWiresClose && OnCheckInRenameClickExecuted &&
                       OnDoneBuildPCExecuted && OnTestPCExecuted && OnTurnOnExecuted &&
                       OnTurnOffExecuted && OnComputerInvExecuted && OnComputerCloseExecuted &&
                       OnOrdersOpenExecuted && OnSubmitExecuted && OnEndExecuted;

        if (allExecuted)
        {
            PlayerPrefs.SetInt("TutorialDone", 1);
        }
        else
        {
            PlayerPrefs.SetInt("TutorialDone", 0);
        }
    }
    public void Start()
    {
        // Ensure the DialogueManager is properly referenced in the Inspector
        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
        }
        shopButton.interactable = false;
        BuildButton.interactable = false;
        BuildBackButton.interactable = false;
        OrdersButton.interactable = false;
        ComputerButton.interactable = false;
        inventoryButton.interactable = false;
        ShopinBuildButton.interactable = false;
        CaseButton.interactable = false;
        useBTN.interactable = false;
        SellBTN.interactable = false;
        BackHomeBTN.interactable = false;
        invexitButton.interactable = false;
        MainMenuBTN.interactable = false;
        testPCBTN.interactable = false;
        ModifyPCBtn.interactable = false;
        successCloseBTN.interactable = false;
        returnBTN.gameObject.SetActive(false);
    }
    string playerName;
    public void Update()
    {
        CheckTutorial();
        playerName = GameManager.Instance.PlayerName;
        if (OnPlayClickExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {

            if (!orBtn)
            {
                OrdersButton.interactable = true;
                orBtn = true;
            }
        }
       
        if (OnOrdersHideClickExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            BuildButton.interactable = true;
            BuildBackButton.interactable = true;
        }
        if (OnBuildRoomClickExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            inventoryButton.interactable = true;
        }
        if (OnInventoryClickExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            ShopinBuildButton.interactable = true;
        }
        if (OnShopCloseExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
           invexitButton.interactable = true;
        }
        if (OnInventoryClickCloseExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            CaseButton.interactable = true;
            useBTN.interactable = true;
            
        }
        if (OnWiresExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        { 
            if (!ps)
            {
                OnWiresClose = false;
                ps = true;
            }
        }
        if (OnDoneBuildPCExecuted && OnCheckInRenameClickExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            BackHomeBTN.interactable = true;
        }
        if (OnTurnOffExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            ComputerButton.interactable = true;
            testPCBTN.interactable = false;
            ModifyPCBtn.interactable = false;
        }
        if (OnComputerCloseExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            OrdersButton.interactable = true;
        }
        if (OnSubmitExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            OnEnd();
        }
        if (OnEndExecuted && dialogueManager != null && !dialogueManager.dialogueIsOn)
        {
            LTA.HideSubmit();
            LTA.HideOrders();
            GameManager.Instance.DialogUI.SetActive(false);
            GameManager.Instance.Dialog.SetActive(false);
            shopButton.interactable = true;
            BuildButton.interactable = true;
            BuildBackButton.interactable = true;
            OrdersButton.interactable = true;
            ComputerButton.interactable = true;
            inventoryButton.interactable = true;
            ShopinBuildButton.interactable = true;
            CaseButton.interactable = true;
            useBTN.interactable = true;
            SellBTN.interactable = true;
            BackHomeBTN.interactable = true;
            invexitButton.interactable = true;
            MainMenuBTN.interactable = true;
            testPCBTN.interactable = true;
            ModifyPCBtn.interactable = true;
            successCloseBTN.interactable = true;
            returnBTN.gameObject.SetActive(true);

        }
        ThermalOpened();
        WiresOpened();
        WiresConnected();
    }
    //special dialogues
    public void ThermalOpened()
    {
        if (thermalCanvas.isActiveAndEnabled)
        {
            if (!OnThermalExecuted)
            {
                if (dialogueManager != null)
                {
                    // Clear previous dialogue and start a new one
                    dialogueManager.StopDialogue();

                    // Add sentences for button 1's dialogue
                    dialogueManager.sentences.Clear();
                    DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                    newDialogueCharacter.characterName = narratorName;
                    newDialogueCharacter.characterPhoto = player;

                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "This is the Applying Thermal Paste Scene",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });

                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "Just fill In the Square and Reach 100 Percent to Continue... and Click the <color=#00FF00>Done Button</color>",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });

                   

                    // Start the dialogue
                    dialogueManager.StartDialogue();
                }
                OnThermalExecuted = true;
            }
        }
    }
    public void WiresOpened()
    {
        if (GameManager.Instance.WiresSceneEnabled)
        {
            if (!OnWiresExecuted)
            {
                if (dialogueManager != null)
                {
                    // Clear previous dialogue and start a new one
                    dialogueManager.StopDialogue();

                    // Add sentences for button 1's dialogue
                    dialogueManager.sentences.Clear();
                    DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                    newDialogueCharacter.characterName = narratorName;
                    newDialogueCharacter.characterPhoto = player;

                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "This is the Connecting of PSU Wires Scene",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });

                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "Just connect the wires to their Respective Parts, just like you are playing connect the dots...This will close if you already finish connecting Wires",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });

                    // Start the dialogue
                    dialogueManager.StartDialogue();
                }
                OnWiresExecuted = true;
            }
        }
    }
    public void WiresConnected()
    {
        if (!GameManager.Instance.WiresSceneEnabled)
        {
            if (!OnWiresClose)
            {
                if (dialogueManager != null)
                {
                    // Clear previous dialogue and start a new one
                    dialogueManager.StopDialogue();

                    // Add sentences for button 1's dialogue
                    dialogueManager.sentences.Clear();
                    DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                    newDialogueCharacter.characterName = narratorName;
                    newDialogueCharacter.characterPhoto = player;

                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "Once your Done placing The needed Parts, Click the <color=#1E90FF>Done button</color> at the bottom, this will prompt you to change the PCname",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });
                    // Start the dialogue
                    dialogueManager.StartDialogue();
                }
                OnWiresClose = true;
            }
        }
    }
    public float delayTime = 1f;
    public void OnPlayClick()
    {
        StartCoroutine(UpdateNameWithDelay());
       
    }

    IEnumerator UpdateNameWithDelay()
    {
        // Fetch the latest player name with a delay
        yield return new WaitForSeconds(delayTime);

        playerName = GameManager.Instance.PlayerName;

        if (!OnPlayClickExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Hello! Welcome to PCrush!, " + "<color=#00FFFF>"+playerName+"</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
               {
                   dialogueCharacter = newDialogueCharacter,
                 sentence = "Here, you can build your dream computer while learning and enjoying the process...",
                   sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                   sentence = "PCMoneys are the currency used to purchase computer parts.",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You're currently in the Testing Room. To proceed, Click the <color=#FF1493>Orders Button</color> to View the Client Orders",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Add more sentences as needed...

                // Start the dialogue
                dialogueManager.StartDialogue();
            }

            OnPlayClickExecuted = true;
        }
    }

    // tutorial dialogues
    //public void OnPlayClick()
    //{
    //    string name = playerName;
    //    Debug.LogError(playerName);

    //    if (!OnPlayClickExecuted)
    //    {
    //        if (dialogueManager != null)
    //        {
    //            // Clear previous dialogue and start a new one
    //            dialogueManager.StopDialogue();

    //            // Add sentences for button 1's dialogue
    //            dialogueManager.sentences.Clear();
    //            DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

    //            newDialogueCharacter.characterName = narratorName;
    //            newDialogueCharacter.characterPhoto = player;

    //            dialogueManager.sentences.Add(new NPC_Centence
    //            {
    //                dialogueCharacter = newDialogueCharacter,
    //                sentence = "Hello! Welcome to PCrush!, " + name,
    //                sentenceSound = null,
    //                sentenceEvent = defaultEvent
    //            });
    //            dialogueManager.sentences.Add(new NPC_Centence
    //            {
    //                dialogueCharacter = newDialogueCharacter,
    //                sentence = "In Here You can build your dream computer at the same time, learning and enjoy from It...",
    //                sentenceSound = null,
    //                sentenceEvent = defaultEvent
    //            });
    //            dialogueManager.sentences.Add(new NPC_Centence
    //            {
    //                dialogueCharacter = newDialogueCharacter,
    //                sentence = "PCMoneys are the currency used to buy parts.",
    //                sentenceSound = null,
    //                sentenceEvent = defaultEvent
    //            });
    //            dialogueManager.sentences.Add(new NPC_Centence
    //            {
    //                dialogueCharacter = newDialogueCharacter,
    //                sentence = "You are in The Testing Room, To Continue. Click the Orders Button to View the Client Orders",
    //                sentenceSound = null,
    //                sentenceEvent = defaultEvent
    //            });
    //            // Add more sentences as needed...

    //            // Start the dialogue
    //            dialogueManager.StartDialogue();
    //        }


    //        OnPlayClickExecuted = true;
    //    }
    //}


    public void OnOrdersClick()
    {
        if (!OnOrdersClickExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "These are your clients who want a computer.",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Each client has their own computer requirements, price, and time limit.",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Higher-priced client orders have shorter time limits, making you to be fast in PC Building",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Clients spawn randomly, and the chance of having a higher price is minimal.",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "If you encounter a client that you consider a good deal, build a computer as fast as possible",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Then, submit it to them to earn PCMoney and Experience making you to level Up",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "So, if you're unfamiliar with PC building, don't worry. I'll teach you here in PCRush",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "But before that, close this page first so we can Continue... Click the <color=#00FF00>Close Button</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnOrdersClickExecuted = true;
            if (PlayerPrefs.GetInt("TutorialDone") == 1) {
                OrdersButton.interactable = true;
            }
            else
            {
                OrdersButton.interactable = false;
            }
           
        }
    }
    public void OnOrdersHideClick()
    {
        if (!OnOrdersHideClickExecuted)
        {


            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Go to The Building Room where we can build your First Computer, Click the <color=#FFFF00>Build Button</color> or The <color=#FFFF00>Arrow in the Right</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                // Start the dialogue
                 dialogueManager.StartDialogue();
            }
            OnOrdersHideClickExecuted = true;
         }
        }
        public void OnBuildRoomClick()
       {
        if (!OnBuildRoomClickExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Welcome To Building Room, In here you can Build Your Own Computer buy using PCrush Computer Parts",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "To Build A Computer, Of Course you Need Computer Parts...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "This Parts are CASE, MOTHERBOARD, CPU, CPU FAN, RAM, GPU, STORAGE, AND PSU",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You can view the meaning of those Acronyms in the Game Glossary, But for now...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Check your Parts Inventory First by clicking the <color=#00FFFF>Inventory Button</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                // Start the dialogue
                dialogueManager.StartDialogue();
            }

            OnBuildRoomClickExecuted = true;
        }
    }
    public void OnInventoryClick()
    {
        if (!OnInventoryClickExecuted) { 

        if (dialogueManager != null)
        {
            // Clear previous dialogue and start a new one
            dialogueManager.StopDialogue();

            // Add sentences for button 1's dialogue
            dialogueManager.sentences.Clear();
            DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

            newDialogueCharacter.characterName = narratorName;
            newDialogueCharacter.characterPhoto = player;

            dialogueManager.sentences.Add(new NPC_Centence
            {
                dialogueCharacter = newDialogueCharacter,
                sentence = "You don't have any Computer Parts yet...",
                sentenceSound = null,
                sentenceEvent = defaultEvent
            });

            dialogueManager.sentences.Add(new NPC_Centence
            {
                dialogueCharacter = newDialogueCharacter,
                sentence = "We Give you a free $10000 PCMoney for you to Start building A Computer",
                sentenceSound = null,
                sentenceEvent = defaultEvent
            });

            dialogueManager.sentences.Add(new NPC_Centence
            {
                dialogueCharacter = newDialogueCharacter,
                sentence = "Now Go to PCRush Shop and Buy Some Computer Parts... Click the <color=#00FF00>Shop Button</color>",
                sentenceSound = null,
                sentenceEvent = defaultEvent
            });

            // Start the dialogue
            dialogueManager.StartDialogue();
        }
            OnInventoryClickExecuted = true;
            OnInventoryClickAgainExecuted = true;
            //OnInventoryClickCloseExecuted = true;
            
        }
    }
    public void OnShopClick()
    {
        if (!OnShopClickExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Welcome to the PCRush Shop!",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Here, you'll find plenty of computer parts available for purchase using PCMoneys.",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "To Build A Computer, You need a CASE, MOTHERBOARD, CPU, CPU FAN, RAM, GPU, STORAGE, and PSU",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Now, Buy the Computer Parts on each Categories, you can sort those categories by clicking the Dropdown in Upper Right Corner ",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Click the <color=#FF0000>X Button</color> to close The Shop, if you are done buying those needed Parts...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnShopClickExecuted = true;
        }
    }
    public void OnCloseShopClick()
    {
        if (!OnShopCloseExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Now check your Parts Inventory Again... Click <color=#00FFFF>Inventory Button</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnInventoryClickAgainExecuted = false;
            OnShopCloseExecuted = true;
        }
    }
    public void OnInventoryClickAgain()
    {
        if (!OnInventoryClickAgainExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You can Click the Computer Part to View its Name, Category, and Description....",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You can also Use and Sell the item by Clicking the Buttons",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Now Close the Inventory by Clicking the <color=#FF0000>X Button</color> and We Will start Building Your First Computer",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnInventoryClickAgainExecuted = true;
            OnInventoryClickCloseExecuted = false;
        }
    }
    public void OnInventoryClickClose()
    {
        if (!OnInventoryClickCloseExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "The Steps is Simple, Just follow the Buttons that will be interactable...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Wait for the <color=#FFA500>Case button</color> to become interactive. Once it opens the inventory for cases, select the one you purchased from the shop.",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Then, Click the <color=#FFFF00>Use Button</color>  ",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                // Start the dialogue
                dialogueManager.StartDialogue();
            }
        }
        OnInventoryClickCloseExecuted = true;
    }
    public void OnUseClick()
    {
        if (!OnUseClickExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;
                string motherboardColor = "<color=#FF00FF>Motherboard</color>";
                string cpuColor = "<color=#FFFF00>CPU</color>";
                string cpuFanColor = "<color=#00FFFF>CPU Fan</color>";
                string ramColor = "<color=#1E90FF>RAM</color>";
                string gpuColor = "<color=#FFFF00>GPU</color>";
                string storageColor = "<color=#00FFFF>Storage</color>";
                string psuColor = "<color=#1E90FF>PSU</color>";

                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = $"Now you are successfully placed your Case in your table, now place your {motherboardColor}, {cpuColor}, {cpuFanColor}, {ramColor}, {gpuColor}, {storageColor}, and {psuColor}",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnUseClickExecuted = true;
        }
    }
    public void OnDoneThermalClick()
    {
        if (!OnDoneThermalClickExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Continue Placing the Needed Parts...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnDoneThermalClickExecuted = true;
        }
    }
    public void OnCheckInRenameClick()
    {
        if (!OnCheckInRenameClickExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Once your done changing the PCName, click the <color=#1E90FF>Done button</color> again...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnCheckInRenameClickExecuted = true;
            OnDoneBuildPCExecuted = false;
        }
    }
    public void OnDoneBuildPCClick()
    {
        if (!OnDoneBuildPCExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Congratulations!",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You Successfully Build your First Computer",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Now We can Test your Built Computer in Testing Room, Just click the <color=#1E90FF>Test Computer Button</color> ",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnDoneBuildPCExecuted = true;
        }
    }
    public void OnTestPCClick()//this should be testCompBTN and Arrow
    {
        if (!OnTestPCExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "In here, You can now Test Your Computer",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "As you can See, Your Computer is Untested",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Click The <color=#00FF00>Turn On Button</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });



                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnTestPCExecuted = true;
        }
    }
    public void OnTurnOnClick()//this should be testCompBTN and Arrow
    {
        if (!OnTurnOnExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "After you clicked the Turn On button it will Open your Computer and It will become <color=#00FF00>Tested</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You can also Click The <color=#FF0000>Turn Off Button</color>, to Turn Off the Computer...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });


                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnTurnOnExecuted = true;
        }
    }
    public void OnTurnOffClick()//this should be testCompBTN and Arrow
    {
        if (!OnTurnOffExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You Can view your built Computers in Computers Inventory...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Just Click the <color=#1E90FF>Computers Button</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });


                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnTurnOffExecuted = true;
        }
    }
    public void OnComputerInvClick()//this should be testCompBTN and Arrow
    {
        if (!OnComputerInvExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "This is where your Computer is Stored...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You can Click the Computer to View its Parts, Building Cost and Name...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "And also You can Test the Computer and Modify it...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "For now close your Computer Inventory first, Click the <color=#FF0000>X Button</color>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });



                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnComputerInvExecuted = true;
            XBtn = false;
        }
    }
    public void OnComputerCloseClick()//this should be testCompBTN and Arrow
    {
        if (!XBtn)
        {
            if (!OnComputerCloseExecuted)
            {
                if (dialogueManager != null)
                {
                    // Clear previous dialogue and start a new one
                    dialogueManager.StopDialogue();

                    // Add sentences for button 1's dialogue
                    dialogueManager.sentences.Clear();
                    DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                    newDialogueCharacter.characterName = narratorName;
                    newDialogueCharacter.characterPhoto = player;


                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "And Now, You already have a Built and Tested Computer...",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });
                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "it's Time to Earn Some PCMoney and Experience",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });
                    dialogueManager.sentences.Add(new NPC_Centence
                    {
                        dialogueCharacter = newDialogueCharacter,
                        sentence = "Now Open Again the Client Orders, by Clicking The <color=#FF00FF>Orders Button</color>",
                        sentenceSound = null,
                        sentenceEvent = defaultEvent
                    });




                    // Start the dialogue
                    dialogueManager.StartDialogue();
                }
                OnComputerCloseExecuted = true;
                OnOrdersOpenExecuted = false;
            }
        }
    }
    public void OnOrdersOpenClick()
    {
        if (!OnOrdersOpenExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Now, <color=#00FFFF>click the client</color> you believe has a good deal for your built computer",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Next, select the computer that you want to submit.",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Then, Click the <color=#00FF00>Submit Button</color>, To Claim the PCMoney and Experience",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                // Start the dialogue
                dialogueManager.StartDialogue();
            }
            OnOrdersOpenExecuted = true;
        }
    }
    public void OnSubmitClick()//this should be testCompBTN and Arrow
    {
        if (!OnSubmitExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Congratulations!",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "You successfully Submitted your First Computer to the Client...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                

                dialogueManager.StartDialogue();
            }
            OnSubmitExecuted = true;
        }
    }
    public void OnEnd()
    {
        if (!OnEndExecuted)
        {
            if (dialogueManager != null)
            {
                // Clear previous dialogue and start a new one
                dialogueManager.StopDialogue();

                // Add sentences for button 1's dialogue
                dialogueManager.sentences.Clear();
                DialogueCharacter newDialogueCharacter = ScriptableObject.CreateInstance<DialogueCharacter>();

                newDialogueCharacter.characterName = narratorName;
                newDialogueCharacter.characterPhoto = player;


                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Now, you're all set to build more computers!...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Remember to keep exploring and learning about computer building...",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Once again, a warm welcome to PCRUSH",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });
                dialogueManager.sentences.Add(new NPC_Centence
                {
                    dialogueCharacter = newDialogueCharacter,
                    sentence = "Goodbye :>",
                    sentenceSound = null,
                    sentenceEvent = defaultEvent
                });

                dialogueManager.StartDialogue();
            }
            OnEndExecuted = true;
        }
    }
}