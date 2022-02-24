using UnityEngine;


namespace BlueHood.Utility.Interfaces
{
    public interface IDamageable
    {
        public GameObject GameObject { get; }
        public Transform Transform { get; }

        
        public void PerformDamage(int amount);
        public void ApplyForce(Vector2 direction, float power, ForceMode2D force = ForceMode2D.Force);
    }
}
