using Inventory.UI;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace PC.UI
{
    public class PCPage : MonoBehaviour
    {
        [SerializeField]
        private PCItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private PCDesc PCDescription;

        [SerializeField]
        private Sprite UntestedImage;

        [SerializeField]
        private Sprite TestedImage;


        public List<PCItem> ListOfPCs = new List<PCItem>();

        //public Sprite PCimage, pcase, pmb, pcpu, pcpuf, pram, pgpu, pstrg, ppsu;
        //public string pcname, pcprice, casen, mbn, cpun, cpufn, ramn, gpun, strgn, psun;
        public event Action<int> OnDescriptionRequested, OnItemActionRequested;
        public void UpdateDescription(int index,Sprite pcsprite, Sprite casesprite, Sprite mbsprite, Sprite cpusprite, Sprite cpufsprite, Sprite ramsprite, Sprite gpusprite, Sprite strgsprite, Sprite psusprite,
        string PCname, string PCprice, string Casename, string mbname, string cpuname, string cpufname, string ramname, string gpuname, string strgname, string psuname, string status)
        {

            PCDescription.SetDescription(pcsprite, casesprite,mbsprite,  cpusprite, cpufsprite, ramsprite,  gpusprite, strgsprite, psusprite,
            PCname,  PCprice, Casename, mbname,  cpuname,  cpufname,  ramname,  gpuname, strgname, psuname, status);
            DeselectAllItems();
            ListOfPCs[index].Select();

        }

        public void Start()
        {
            ResetSelection();
            //ListOfPCs[0].SetData(PCimage, pcname);
        }

        public void ResetSelection()
        {

            PCDescription.Hide();
            PCDescription.ResetDescription();
            DeselectAllItems();
            
        }

        private void DeselectAllItems()
        {
            foreach (PCItem item in ListOfPCs)
            {
                item.DeSelect();
            }
        }
        public void AddAnotherPC()
        {
            PCItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            ListOfPCs.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseBtnClick += HandleRightClickAction;
        }
        public void Awake()
        {
           
            //PCDescription.ResetDescription();
        }
        public void InitializedPCs(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                PCItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                ListOfPCs.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleRightClickAction;

            }
        }
        public void UpdateData(int ItemIndex, Sprite PCimage, string PCname, string testStatus)
        {
            if(ListOfPCs.Count > ItemIndex)
            {
                if (testStatus == "Untested")
                {
                    ListOfPCs[ItemIndex].SetData(UntestedImage,PCimage, PCname);
                }
                if (testStatus == "Tested")
                {
                    ListOfPCs[ItemIndex].SetData(TestedImage, PCimage, PCname);
                }

            }
        }
        private void HandleItemSelection(PCItem item)
        {
            PCDescription.Show();
            int index = ListOfPCs.IndexOf(item);
            if(index == -1)
            {
                return;
            }
            OnDescriptionRequested?.Invoke(index);

        }
        private void HandleRightClickAction(PCItem item)
        {
            int index = ListOfPCs.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        /* public void Show()
         {
             gameObject.SetActive(true);

         }
         public void Hide()
         {
             gameObject.SetActive(false);
         }*/


    }


}

