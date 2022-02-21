using BlueHood.Managers;
using UnityEngine;


namespace BlueHood.Items.Pickables
{
    public class Coin : PickableItem, Data.ISpawnable
    {
        [Space]
        [SerializeField] private int _coinAmount = 1;
        
        public Rigidbody2D Body { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            
            Body = GetComponent<Rigidbody2D>();
        }


        protected override void OnUse()
        {
            LevelManager.Instance.AddCoin(_coinAmount);
        }
    }
}