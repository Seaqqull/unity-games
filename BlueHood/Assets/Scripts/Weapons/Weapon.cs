using UnityEngine;


namespace BlueHood.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _pivot;
        [SerializeField] protected GameObject _hitObject;
        [Space]
        [SerializeField] protected LayerMask _enemyLayer;
        [SerializeField] protected float _range;

        protected Transform _transform;
        protected float _rangeRadius;
        
        
        protected virtual void Awake()
        {
            _rangeRadius = (_range * 0.5f);
            _transform = transform;
        }
        
        private void OnDrawGizmosSelected()
        {
            var oldColor = Gizmos.color;
            
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(((_pivot != null) ? _pivot.position : transform.position), (_range * 0.5f));

            Gizmos.color = oldColor;
        }


        protected void SpawnHitObject(Vector2 hitPosition)
        {
            Instantiate(_hitObject, hitPosition, Quaternion.identity);
        }


        protected abstract void ProcessHittedTarget(RaycastHit2D target);


        public void Shoot()
        {
            var targets = Physics2D.CircleCastAll(_pivot.position, _rangeRadius, Vector2.zero, 0.0f, _enemyLayer);
            if (targets.Length == 0)
                return;

            var meanPosition = Vector2.zero;
            foreach (var target in targets)
            {
                ProcessHittedTarget(target);
                meanPosition += target.point;
            }
            
            if(_hitObject != null)
                SpawnHitObject(meanPosition / targets.Length);
        }
    }
}
