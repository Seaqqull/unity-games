using BlueHood.Items.Data;
using BlueHood.Behaviour;
using UnityEngine;


namespace BlueHood.Items.Pickables
{
    public abstract class PickableItem : Item, IUsable, IPickable
    {
        [SerializeField] protected bool _singleUse = true;
        [SerializeField] protected bool _isPickable;


        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Player>() == null)
                return;
            
            Use();
            if(_singleUse)
                Destroy(gameObject);
        }


        protected abstract void OnUse(); 
        
        
        public virtual void Use()
        {
            if(_isPickable)
                OnUse();
        }
        
        public IUsable GetUsable()
        {
            return this;
        }
        
        public virtual bool SetPickable(bool flag)
        {
            if(flag == _isPickable) return false;

            _isPickable = flag;

            return true;
        }
        
    }
}