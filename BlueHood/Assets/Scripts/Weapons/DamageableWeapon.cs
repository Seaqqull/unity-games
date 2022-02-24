using BlueHood.Utility.Interfaces;
using UnityEngine;


namespace BlueHood.Weapons
{
    public class DamageableWeapon : Weapon
    {
        [Space]
        [SerializeField] protected int _damage;


        protected virtual void OnTargetHit(IDamageable target)
        {
            target.PerformDamage(_damage);
        }


        protected override void ProcessHittedTarget(RaycastHit2D target)
        {
            var damageableTarget = target.collider.gameObject.GetComponent<IDamageable>();
            if(damageableTarget != null)
                OnTargetHit(damageableTarget);
        }
    }
}