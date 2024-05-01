using Inventory.Model;
using Inventory.UI;
using Orders.Model;
using Orders.UI;
using PC;
using PC.Model;
using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

namespace Orders
{
    public class OrdersController : MonoBehaviour
    {
        
        [SerializeField]
        private OrdersPage OrdersPages;

        [SerializeField]
        private PCPlayerController PCcontrol;

        [SerializeField]
        private TMP_Text visibleMissionCountText;

        [SerializeField]
        private PCMenu menu;

        public TMP_Text SubmitToName;

        public Button CloseBTN;

       
        [SerializeField]
        private MissionConSO OrderData;

        public Dictionary<int, float> remainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> missionDisplayed = new Dictionary<int, bool>();

        public int currentlevel;

        private Queue<int> missionsToReplace = new Queue<int>();




        void Start()
        {

            currentlevel = GameManager.Instance.currentLevel;
            OrderData.ShuffleMissions();
            OrdersPages.InitializedMissions(OrderData.size);

            var currentInventoryState = OrderData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
             remainingTimes[item.Key] = item.Value.orders.TimeLimit; // Retrieve time limit for each miss                                                     //OrdersPages.UpdateData(item.Key, item.Value.orders.ClientImage, item.Value.orders.ClientName, item.Value.orders.Description, item.Value.orders.Requirements, remainingTimes[item.Key], item.Value.orders.OrderPrice);
             missionDisplayed[item.Key] = false;
            }

            StartCoroutine(UpdateTimers());
            OrdersPages.OnOpenPCInventory += OpenComputerInventory;
            GameManager.Instance.UpdatePCMoneyText(); // Update the PCMoney text UI

            CloseBTN.onClick.AddListener(() => { 
            
            
            selectedMissionIndex = -1;
                menu.Hide();
            
            
            });
        }
        
        private IEnumerator UpdateTimers()
        {
           
            while (true)
            {
                yield return new WaitForSeconds(1f);

                List<int> missionsToRespawn = new List<int>();

                int visibleMissionCount = 0;

              
                    //UpdateMissionsTime();
                    foreach (var missionId in remainingTimes.Keys.ToList()) // Iterate through mission IDs
                    {

                        if (IsMissionDisplayed(missionId)) // Check if the mission is displayed
                        {
                            remainingTimes[missionId] -= 1f;

                            if (remainingTimes[missionId] <= 0f)
                            {
                                missionsToRespawn.Add(missionId);

                            }
                            visibleMissionCount++;
                        }

                    
                }

                if (missionsToRespawn.Count > 0)
                {

                    foreach (var missionIds in missionsToRespawn)
                    {
                        RespawnMission(missionIds , OrderData.size);//it starts from 0, so 0,1,2,3 = 4
                    }
                }
                if (visibleMissionCountText != null)
                {
                    visibleMissionCountText.text = visibleMissionCount.ToString();
                }

               
                OrderData.OnMissionUpdated += UpdateMissionUI;
                OrderData.InformAboutChange();

            }
        }
        public int randomInt;
        private bool IsMissionDisplayed(int missionId)
        {
            // Implement your logic here based on your UI setup
            // For example, if your mission UI is a panel or game object:
            return OrdersPages.IsMissionVisible(missionId);
        }


        //public void RespawnMission(int missionId)
        //{
        //    var missionToMove = OrderData.GetItemAt(missionId);

        //    OrderData.RemoveMission(missionId);
        //    OrderData.missionOrders.Add(missionToMove);
        //    remainingTimes[missionId] = missionToMove.orders.TimeLimit;

        //    OrderData.InformAboutChange();
        //}

        //public void RespawnMission(int lastMissionIndex, int newMissionIndex)
        //{
        //    var lastMissionToMove = OrderData.GetItemAt(lastMissionIndex);
        //    var newMissionToMove = OrderData.GetItemAt(newMissionIndex);

        //    OrderData.missionOrders.Add(lastMissionToMove);

        //    OrderData.ReplaceMission(lastMissionIndex, newMissionToMove);


        //    OrderData.RemoveMission(newMissionIndex);

        //    remainingTimes[lastMissionIndex] = newMissionToMove.orders.TimeLimit;

        //    //OrderData.InformAboutChange();


        //}

        //var lastMissionToMove = OrderData.GetItemAt(lastMissionIndex);
        //var newMissionToMove = OrderData.GetItemAt(newMissionIndex);

        //OrderData.missionOrders.Add(lastMissionToMove);
        //OrderData.ReplaceMission(lastMissionIndex, newMissionToMove);
        //OrderData.RemoveMission(newMissionIndex);

        //remainingTimes[lastMissionIndex] = newMissionToMove.orders.TimeLimit;
        //private IEnumerator ReplaceMissionWithDelay(int lastMissionIndex, int MissionToBeShowed, float delay)
        //{

        //    var lastMissionToMove = OrderData.GetItemAt(lastMissionIndex);
        //    var MissionToBeShow = OrderData.GetItemAt( MissionToBeShowed);

        //    if (MissionToBeShow.orders.Level > currentlevel)
        //    {
        //        yield return new WaitForSeconds(.1f);
        //        OrderData.missionOrders.Add(MissionToBeShow);
        //        OrderData.RemoveMission(MissionToBeShowed);
               
               
        //    }
        //    else
        //    {
        //        yield return new WaitForSeconds(delay);
        //        OrderData.missionOrders.Add(lastMissionToMove);
        //        OrderData.ReplaceMission(lastMissionIndex, MissionToBeShow);
        //        OrderData.RemoveMission(MissionToBeShowed);

        //        remainingTimes[lastMissionIndex] = MissionToBeShow.orders.TimeLimit;
                
        //    }
        //    // OrderData.InformAboutChange(); // Uncomment this line if needed

        //}

        public void RespawnMission(int lastMissionIndex, int MissionToBeShowed)
        {
            //float delay = 1f; // Change this value to set your desired delay in seconds
            //StartCoroutine(ReplaceMissionWithDelay(lastMissionIndex, MissionToBeShowed, delay));
            var lastMissionToMove = OrderData.GetItemAt(lastMissionIndex);
            var MissionToBeShow = OrderData.GetItemAt(MissionToBeShowed);

            if (MissionToBeShow.orders.Level > currentlevel)
            {
               // yield return new WaitForSeconds(.1f);
                OrderData.missionOrders.Add(MissionToBeShow);
                OrderData.RemoveMission(MissionToBeShowed);


            }
            else
            {
                //yield return new WaitForSeconds(delay);
                OrderData.missionOrders.Add(lastMissionToMove);
                OrderData.ReplaceMission(lastMissionIndex, MissionToBeShow);
                OrderData.RemoveMission(MissionToBeShowed);

                remainingTimes[lastMissionIndex] = MissionToBeShow.orders.TimeLimit;

            }

        }


        //public void MissionPrice(int missionIndex)
        //{
        //    var mission = OrderData.GetItemAt(missionIndex);
        //    GameManager.Instance.PCMoney += Convert.ToInt32(mission.orders.OrderPrice);
        //    GameManager.Instance.SavePCMoney(); // Save the updated PCMoney
        //    GameManager.Instance.UpdatePCMoneyText(); // Update the PCMoney text UI
        //}
        

       

       
        private void UpdateMissionUI(Dictionary<int, Missions> inventoryState)
        {
            //inventoryUI.ResetAllItems();
            
                foreach (var item in inventoryState)
                {
                    string requirementsNames = string.Join("\n- ", item.Value.orders.Requirements.Select(req => req.Name));
                    requirementsNames = "- " + requirementsNames.Replace("\n", "\n");

                if (remainingTimes[item.Key] <= 0)
                {
                    remainingTimes[item.Key] = 0;
                }

                    OrdersPages.UpdateData(item.Key, item.Value.orders.ClientImage, item.Value.orders.ClientName, item.Value.orders.Description, requirementsNames, remainingTimes[item.Key], item.Value.orders.OrderPrice, item.Value.orders.EXP, item.Value.orders.Level);
                    if (remainingTimes[item.Key] <= 10f)
                    {
                        OrdersPages.SetTimeTextColor(item.Key, Color.red); // Set the time text color to red
                    }
                    else
                    {
                        OrdersPages.SetTimeTextColor(item.Key, Color.white); // Set the time text color to its default color
                    }
                }
            
            

        }
        public int selectedMissionIndex = -1;
        private void OpenComputerInventory(int missionId)
        {
            //open pc
            //PCinv.SetActive(true);
            PCcontrol.InfoButton.gameObject.SetActive(false);
            PCcontrol.SubmitPanel.SetActive(true);
            PCcontrol.TnMPanel.SetActive(false);
            PCcontrol.OpenPCInv();


            var missionToMove = OrderData.GetItemAt(missionId);
            SubmitToName.text = "Submit Computer to " + missionToMove.orders.ClientName + "?";
            selectedMissionIndex = missionId;
        }
       
        public void IfSelectedMissionIs0()
        {
            try
            {
                var mission = OrderData.GetItemAt(selectedMissionIndex);
                if (remainingTimes[selectedMissionIndex] <= 0)
                {
                    if (menu.isActiveAndEnabled)
                    {
                        menu.Hide();
                        PCcontrol.DialogBox.gameObject.SetActive(true);
                        PCcontrol.YesButton.gameObject.SetActive(false);
                        PCcontrol.NoButton.gameObject.SetActive(false);
                        PCcontrol.exitButton.gameObject.SetActive(true);
                        PCcontrol.DialogText.text = "Time Limit Reached for " + mission.orders.ClientName + "...";

                    }
                    selectedMissionIndex = -1;
                }
            }catch (Exception) { }


        }

        //private IEnumerator DelayedRestartTimerCoroutine()
        //{
        //    yield return new WaitForSeconds(1f);

        //    StopAllCoroutines();
        //    remainingTimes.Clear();
        //    missionDisplayed.Clear();
        //    OrdersPages.ListofMissions.Clear();

        //    OrderData.AddMissionByLevel(GameManager.Instance.currentLevel);

        //    OrderData.ShuffleMissions();
        //    OrdersPages.InitializedMissions(size);

        //    var currentInventoryState = OrderData.GetCurrentInventoryState();
        //    foreach (var item in currentInventoryState)
        //    {
        //        remainingTimes[item.Key] = item.Value.orders.TimeLimit;
        //        missionDisplayed[item.Key] = false;
        //    }

        //    StartCoroutine(UpdateTimers());
        //    GameManager.Instance.UpdatePCMoneyText();
        //}

        //private void RestartTimerCoroutineWithDelay()
        //{
        //    StartCoroutine(DelayedRestartTimerCoroutine());
        //}

        //private void HandleLevelUp()
        //{
        //    if (GameManager.Instance.currentLevel > currentlevel)
        //    {
        //        currentlevel = GameManager.Instance.currentLevel;

        //        // Restart the timer coroutine when the player levels up
        //        RestartTimerCoroutineWithDelay();
        //    }
        //}
        

        public void Update()
        {
            // HandleLevelUp();
            currentlevel = GameManager.Instance.currentLevel;
            IfSelectedMissionIs0();
        }

        
    }
}

