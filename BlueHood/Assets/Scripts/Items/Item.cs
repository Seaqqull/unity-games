using BlueHood.Items.Data;
using UnityEngine;


namespace BlueHood.Items
{
    public class Item : Base.BaseBehaviour
    {
        [SerializeField] private ItemType _type;

        public ItemType Type
        {
            get => _type;
        }
        
    }
}
