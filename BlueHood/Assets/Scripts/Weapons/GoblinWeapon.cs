using BlueHood.Utility.Interfaces;
using UnityEngine;


namespace BlueHood.Weapons
{
    public class GoblinWeapon : DamageableWeapon
    {
        [Space] 
        [SerializeField] private float _forcePower = 1.0f;
        
        
        protected override void OnTargetHit(IDamageable target)
        {
            base.OnTargetHit(target);

            var desiredForce = Vector2.right * 
                               Mathf.Sign(target.Transform.position.x - _transform.position.x);

            target.ApplyForce(desiredForce, _forcePower, ForceMode2D.Impulse);
        }
    }
}
