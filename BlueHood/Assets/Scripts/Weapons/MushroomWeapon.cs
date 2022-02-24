using BlueHood.Utility.Interfaces;
using UnityEngine;


namespace BlueHood.Weapons
{
    public class MushroomWeapon : DamageableWeapon
    {
        [Space] 
        [SerializeField] private float _forcePower = 1.0f;
        
        
        protected override void OnTargetHit(IDamageable target)
        {
            base.OnTargetHit(target);

            var desiredForce = Vector2.Lerp(
                (target.Transform.position - _transform.position), 
                Vector2.up, Random.Range(0.0f, 1.0f)).normalized;

            target.ApplyForce(desiredForce, _forcePower, ForceMode2D.Impulse);
        }
    }
}