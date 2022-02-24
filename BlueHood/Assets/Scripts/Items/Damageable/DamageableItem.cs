using BlueHood.Utility.Interfaces;
using UnityEngine;


namespace BlueHood.Items.Damageable
{
    public abstract class DamageableItem : Item, IDamageable
    {
        [SerializeField] protected int _health;

        public GameObject GameObject
        {
            get => GameObj;
        }
        public Rigidbody2D Body { get; protected set; }
        
        

        protected override void Awake()
        {
            base.Awake();
            
            Body = GetComponent<Rigidbody2D>();
        }


        protected abstract void OnDamage();
        
        
        public void PerformDamage(int amount)
        {
            _health -= amount;
            if (_health < 0)
                _health = 0;
            
            OnDamage();
        }

        public virtual void ApplyForce(Vector2 direction, float power, ForceMode2D force = ForceMode2D.Force)
        {
            
        }
    }
}
