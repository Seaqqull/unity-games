using System;
using UnityEngine;


namespace BlueHood.Behaviour.Enemies
{
    public abstract class Enemy : Entity
    {
        [Header("General")]
        [SerializeField] private Data.Type _type;
        [SerializeField] protected Data.State _state;
        [SerializeField] protected Data.ViewDirection _spectation;
        [Header("Path")]
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Navigation.NavigationContainer _navigation;
        [SerializeField] protected float _movementSpeed = 2.5f;
        [SerializeField] protected float _minimumPathDistance;
        [Header("Target")]
        [SerializeField] protected Transform _target;
        [Space]
        [SerializeField] protected float _minimumTargetDistance;
        [SerializeField] protected float _distanceToTarget;
        [SerializeField] protected Vector2 _targetDetectionDistance;
        [SerializeField] protected bool _onlyDirectDetection = true;
        [SerializeField] private bool _wasInPursuit;

        protected Vector2 _targetDetectionDistanceSquared;
        protected float _minimumTargetDistanceSquared;
        protected float _minimumPathDistanceSquared;
        protected Vector2 _movementDirection;
        protected bool _movementPerformed;
        protected Coroutine _pathDelay;
        protected Collider2D _collider;
        protected int _pathIndex;
        
        protected bool PathValid
        {
            get => (_navigation != null) && (_pathDelay == null);
        }

        public Data.ViewDirection Spectation
        {
            get => _spectation;
        }
        public Vector2 DetectionDistance
        {
            get => _targetDetectionDistanceSquared;
        }
        public bool DirectDetectionOnly
        {
            get => _onlyDirectDetection;
        }
        public bool ReachedPathPoint
        {
            get => (_distanceToTarget <= _minimumPathDistanceSquared);
        }
        public bool InAttackRange
        {
            get => (_distanceToTarget <= _minimumTargetDistanceSquared);
        }
        public Transform Target
        {
            get => _target;
            set
            {
                _target = value;
            }
        }
        
        
        protected override void Awake()
        {
            base.Awake();

            _collider = GetComponent<Collider2D>();
            _distanceToTarget = float.MaxValue;
            _pathIndex = 0;

            _targetDetectionDistanceSquared = Vector2.Scale(_targetDetectionDistance, _targetDetectionDistance);
            _minimumTargetDistanceSquared = _minimumTargetDistance * _minimumTargetDistance;
            _minimumPathDistanceSquared = _minimumPathDistance * _minimumPathDistance;
        }

        protected void Update()
        {
            _state = (_target == null)? Data.State.Moving : Data.State.Chasing;

            if((_target == null) && PathValid && ReachedPathPoint)
            {
                var pathPoint = _navigation.GetPoint(_pathIndex);
                _distanceToTarget = float.MaxValue;

                if (pathPoint.Action == Navigation.Data.PointAction.Continue)
                {
                    _navigation.GetDestination(ref _pathIndex);
                }
                else if (pathPoint.Action == Navigation.Data.PointAction.Stop)
                {
                    _movementPerformed = false;
                    _state = Data.State.Idle;

                    _pathDelay = RunLaterValued(()=> {
                        _navigation.GetDestination(ref _pathIndex);
                        _pathDelay = null;
                    }, pathPoint.TransferDelay);
                }
            }
            if (_target != null && InAttackRange && _state == Data.State.Chasing)
            {
                OnBeginAttack();
            }

            UpdateAnimation();
        }

        protected virtual void FixedUpdate()
        {
            // No enemy to pursuit and not waiting for the next path point
            if((_target == null) && (_pathDelay != null))
                return;

            Vector2 destinationPoint = (_target == null)?
                (_navigation?.GetPathPosition(_pathIndex) ?? Position) : _target.position;

            _movementPerformed = false;

            UpdatePath(destinationPoint);
        }


        protected void DefineSpectation()
        {
            _spectation = (_movementDirection.x > float.Epsilon)?
                Data.ViewDirection.Right :
                (_movementDirection.x < -float.Epsilon)? Data.ViewDirection.Left :
                    _spectation; // Otherwise keep direction
        }

        protected virtual void UpdateAnimation()
        {
            DefineSpectation();

            _animator.SetBool(Utility.Constants.Animation.RUNNING, _movementPerformed);
            _animator.SetInteger(Utility.Constants.Animation.DIRECTION, (_spectation.HasFlag(Data.ViewDirection.Right) ? 1 : -1));
        }

        protected virtual void OnUpdateMovement()
        {
            _movementPerformed = true;
        }

        protected virtual void UpdatePath(Vector2 destination)
        {
            _movementDirection = (destination - _body.position);
            _distanceToTarget = _movementDirection.sqrMagnitude;


            OnUpdatePath(destination);

            if(MovementNeeded())
                OnUpdateMovement();
        }


        protected abstract bool MovementNeeded();
        protected abstract void OnUpdatePath(Vector2 destination);


        public void ResetTargetDistance()
        {
            _distanceToTarget = Single.MaxValue;

        }

        public virtual void OnEndAttack()
        {
            _state = Data.State.Chasing;
            _animator.SetBool(Utility.Constants.Animation.ATTACK, false);
        }

        protected virtual void OnBeginAttack()
        {
            _state = Data.State.Attacking;
            _animator.SetBool(Utility.Constants.Animation.ATTACK, true);
        }

        protected override void OnHealthMinus()
        {
            base.OnHealthMinus();
            
            _animator.SetTrigger(IsDead? Utility.Constants.Animation.DEAD : Utility.Constants.Animation.DAMAGE);
        }
    }
}
