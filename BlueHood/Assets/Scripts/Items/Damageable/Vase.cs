using BlueHood.Items.Data;
using UnityEngine;


namespace BlueHood.Items.Damageable
{
    public class Vase : DamageableItem
    {
        [SerializeField] private Item _itemToSpawn;
        [Space]
        [SerializeField] private float _minThrowAmount;
        [SerializeField] private float _maxThrowAmount;
        [Space] 
        [SerializeField] private int _minAmount; 
        [SerializeField] private int _maxAmount;

        [Header("Additional")] 
        [SerializeField] private Animator _animator;
        
        
        protected override void OnDamage()
        {
            if (_health > 0)
            {
                _animator.SetTrigger(Utility.Constants.Animation.DAMAGE);
                return;
            }
            
            _animator.SetTrigger(Utility.Constants.Animation.DEAD);
        }


        public void OnDestroyDamage()
        {
            var amountToSpawn = Random.Range(_minAmount, _maxAmount);
            for (int i = 0; i < amountToSpawn; i++)
            {
                var item = Instantiate(_itemToSpawn, Transform.position, Quaternion.identity);
                var spawnableItem = item as ISpawnable;
                if (spawnableItem == null)
                    continue;

                var throwDirection = (Vector2.Lerp(Vector2.left, Vector2.up, Random.Range(0.0f, 1.0f)) +
                                      Vector2.Lerp(Vector2.up, Vector2.right, Random.Range(0.0f, 1.0f))).normalized;
                spawnableItem.Body.AddForce(throwDirection * Random.Range(_minThrowAmount, _maxThrowAmount), ForceMode2D.Impulse);
            }
        }
    }
}