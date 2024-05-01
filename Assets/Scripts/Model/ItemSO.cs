using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model {
    [CreateAssetMenu]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackableSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; } //sprite

        [field: SerializeField]
        public string Category { get; set; }
        [field: SerializeField]
        public double Price { get; set; }

    }

}
