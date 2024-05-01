using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Orders.Model
{
    [CreateAssetMenu]
    public class OrderSO : ScriptableObject
    {

        public int ID => GetInstanceID();

        [field: SerializeField]
        public Sprite ClientImage { get; set; }


        [field: SerializeField]
        public string ClientName { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public List<ItemSO> Requirements{ get; set; }

        [field: SerializeField]
        public float OrderPrice { get; set; }

        [field: SerializeField]
        public float TimeLimit { get; set; }

        [field: SerializeField]
        public int EXP { get; set; }

        [field: SerializeField]
        public int Level { get; set; }



    }
}