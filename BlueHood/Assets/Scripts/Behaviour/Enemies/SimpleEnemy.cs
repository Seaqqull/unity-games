using Pathfinding;
using UnityEngine;


namespace BlueHood.Behaviour.Enemies
{
    [RequireComponent(typeof(AIPath))]
    public class SimpleEnemy : Enemy
    {
        protected AIPath _aiPath;
        
        
        protected override void Awake()
        {
            base.Awake();

            _aiPath = GetComponent<AIPath>();
            _aiPath.maxSpeed = _movementSpeed;
        }
        

        protected override bool MovementNeeded()
        {
            return (((_target == null) && (_distanceToTarget > _minimumPathDistance)) ||
                    (_target != null) && (_distanceToTarget > _minimumTargetDistance));
        }

        // protected override void OnUpdateMovement()
        // {
        //     base.OnUpdateMovement();
        //
        //     _aiPath.destination = _target.position;
        // }

        protected override void OnUpdatePath(Vector2 destination)
        {
            _aiPath.destination = destination;
        }
    }
}