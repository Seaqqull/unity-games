using System.Collections;
using UnityEngine;
using System;


namespace BlueHood.Behaviour
{
    public class Entity : Base.BaseBehaviour, Utility.Interfaces.IDamageable, Utility.Interfaces.IRunLater
    {
        [SerializeField] protected  Health _health;
        [SerializeField] protected Transform _gfx;
        [Header("Events")]
        [SerializeField] protected UnityEngine.Events.UnityEvent _onDead;
        
        protected bool _movementPossible;
        protected Rigidbody2D _body;

        public Health Health { get => _health; }
        public bool IsDead {get; protected set;}
        
        
        protected override void Awake()
        {
            base.Awake();

            _body = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
            IsDead = false;

            if(_health != null)
            {
                _health.OnHealthMinus += OnHealthMinus;
                _health.OnHealthPlus += OnHealthPlus;
            }
            else
            {
                _health = Health.HealthNo.Instance;
            }            
        }
        
        
        protected virtual void OnDeath()
        {
            _movementPossible = false;
            IsDead = true;

            _onDead.Invoke();
        }

        protected virtual bool CheckDead()
        {
            if (IsDead) return false;
            return _health.IsZero;
        }

        protected virtual void OnHealthPlus()
        { }

        protected virtual void OnHealthMinus()
        {
            if (CheckDead())
                OnDeath();
        }


        public void PerformDamage(int amount)
        {
            _health.ModifyHealth(amount);
        }

        public virtual void ApplyForce(Vector3 direction, ForceMode2D force = ForceMode2D.Force)
        {
            _movementPossible = false;

            _body.AddForce(direction * _body.mass, force);
        }
        
        
        #region RunLater
        public void RunLater(Action method, float waitSeconds)
        {
            RunLaterValued(method, waitSeconds);
        }

        public Coroutine RunLaterValued(Action method, float waitSeconds)
        {
            if ((waitSeconds < 0) || (method == null))
                return null;

            return StartCoroutine(RunLaterCoroutine(method, waitSeconds));
        }

        public IEnumerator RunLaterCoroutine(Action method, float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            method();
        }        
        #endregion
    }
}
