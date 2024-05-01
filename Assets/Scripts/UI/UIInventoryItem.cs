using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Shop.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
    , IDropHandler, IDragHandler
    {
       
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemQuantity;
        [SerializeField]
        private Image borderImage;

       
        //this whole code is for the border only when its clicked and also when dragged
        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag
            , OnRightMouseBtnClick;
        public event Action<int> OnItemClickeds, OnItemRightClicked; // create new event for filtering

        private bool empty = true;
        public InventoryController IC;

        public void Awake()
        {
            //DontDestroyOnLoad(itemImage);
            //ResetData();
           // DeSelect();
        }
       public void ResetData()
        {

            itemImage.gameObject.SetActive(false);
            empty = true;
        }
        public void DeSelect()
        {
            borderImage.enabled = false;
        }
        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            itemQuantity.text = quantity + "";
            empty = false;
        }
        public void select()
        {
            borderImage.enabled = true;
        }
        // Member variable to store the temporary index
        public int temporaryIndex = 0;
        public void SetTemporaryIndex(int index)
        {
            GameManager.Instance.tempindex = index;
            temporaryIndex = index;
        }
       


        public void OnPointerClick(PointerEventData pointerdata)
        {

            if (pointerdata.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
                OnItemRightClicked?.Invoke(temporaryIndex);
            }
            else
            {
                OnItemClicked?.Invoke(this);
                OnItemClickeds?.Invoke(temporaryIndex);
               
            }
        }
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty) return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}