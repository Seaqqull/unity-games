using BlueHood.Input;
using UnityEngine;


namespace BlueHood.Behaviour
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Vector2 _movementAmount;
        [SerializeField][Range(0.0f, 1.0f)] private float _inAirSpeedScale = 0.5f;
        [SerializeField][Range(0.0f, 1.0f)] private float _groundCheck;

        public bool InputMuted { get; set; }

        private Vector2 _desiredMovement;
        private float _blockInputTime;
        private Collider2D _collider;
        private bool _jumpProceeded;
        private Rigidbody2D _body;
        private bool _grounded;
        private bool _attack;
        private bool _jump;


        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _body = GetComponent<Rigidbody2D>();

            _jumpProceeded = true;
        }
        
        private void Update()
        {
            if (_blockInputTime > 0.0f)
            {
                _blockInputTime -= Time.deltaTime;
                if (_blockInputTime <= 0.0f)
                {
                    _blockInputTime = 0.0f;
                    InputMuted = false;
                }
            }
            if (InputMuted) return;

            float movementInput = InputHandler.Instance.Horizontal;
            bool shiftPressed = InputHandler.Instance.Shift;

            _grounded = CheckGround();

            _jump = _grounded && InputHandler.Instance.Space;
            _attack = InputHandler.Instance.E;
            _desiredMovement = new Vector2(
                movementInput * _movementAmount.x * (_grounded? (shiftPressed? 1.0f : 0.5f) : _inAirSpeedScale), 
                !_jumpProceeded? 0.0f : _movementAmount.y
            );

            UpdateAnimation(movementInput, shiftPressed? 1.0f : 0.0f);
        }

        private void FixedUpdate()
        {
            if (InputMuted)
                return;
            
            
            var moveVector = transform.TransformDirection(Vector2.right) * _desiredMovement.x;
            _body.velocity = new Vector2(moveVector.x, _body.velocity.y);
            
            
            if (!_jumpProceeded)
            {
                _body.AddForce((Vector2.up * _movementAmount.y), ForceMode2D.Impulse);
                _jumpProceeded = true;
            }
        }


        private bool CheckGround()
        {
            return (Physics2D.BoxCast(
                _collider.bounds.center,
                _collider.bounds.size, 
                0.0f, 
                Vector2.down, 
                _groundCheck, 
                _groundMask).collider != null);
        }

        private void UpdateAnimation(float direction, float speed)
        {
            bool isRunning = (direction != 0);
            
            
            _animator.SetBool(Utility.Constants.Animation.GROUNDED, _grounded);
            if(_jump && _grounded)
            {
                _jumpProceeded = false;
                _jump = false;
                
                _animator.SetBool(Utility.Constants.Animation.JUMP, true);
            }
            else if (_attack)
            {
                _attack = false;
                
                _animator.SetBool(Utility.Constants.Animation.ATTACK, true);
            }

            _animator.SetInteger(Utility.Constants.Animation.DIRECTION, (int)direction);
            _animator.SetFloat(Utility.Constants.Animation.WALK_SPEED, speed);
            _animator.SetBool(Utility.Constants.Animation.RUNNING, isRunning);

            if(!_jump && _jumpProceeded && _grounded)
            {
                _animator.SetBool(Utility.Constants.Animation.FALLING, false);
                _animator.SetBool(Utility.Constants.Animation.JUMP, false);
            }
            else if(_body.velocity.y < 0.0f)
            {
                _animator.SetBool(Utility.Constants.Animation.FALLING, true);
            }
        }


        public void OnDead()
        {
            _desiredMovement = Vector2.zero;
            InputMuted = true;

            _animator.SetTrigger(Utility.Constants.Animation.DEAD);
        }
        
        public virtual void OnEndAttack()
        {
            _animator.SetBool(Utility.Constants.Animation.ATTACK, false);
        }

        public virtual void OnBeginAttack()
        {
            _animator.SetBool(Utility.Constants.Animation.ATTACK, true);
        }
        
        public void MuteInput(float period)
        {
            _blockInputTime = period;
            InputMuted = true;
        }
    }
}
