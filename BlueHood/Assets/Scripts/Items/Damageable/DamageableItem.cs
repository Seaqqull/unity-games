using BlueHood.Utility.Interfaces;
using UnityEngine;


namespace BlueHood.Items.Damageable
{
    public abstract class DamageableItem : Item, IDamageable
    {
        [SerializeField] protected int _health;


        protected abstract void OnDamage();
        
        
        public void PerformDamage(int amount)
        {
            _health -= amount;
            if (_health < 0)
                _health = 0;
            
            OnDamage();
        }
    }
}
