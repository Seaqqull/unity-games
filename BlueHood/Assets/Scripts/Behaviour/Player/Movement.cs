using BlueHood.Input;
using UnityEngine;


namespace BlueHood.Behaviour
{
    public class Movement: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Vector2 _movementAmount;
        [SerializeField][Range(0.0f, 1.0f)] private float _groundCheck;

        public bool BlockInput { get; set; }

        private Collider2D _collider;
        private bool _jumpProceeded;
        private Rigidbody2D _body;
        private Vector2 _amount;
        private bool _grounded;
        private bool _jump;


        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _body = GetComponent<Rigidbody2D>();

            _jumpProceeded = true;
        }
        
        private void Update()
        {
            if (BlockInput) return;

            bool shiftPressed = InputHandler.Instance.Shift;
            float direction = InputHandler.Instance.Horizontal;

            _grounded = CheckGround();
            // if (_grounded)
            // {
            //     _animator.SetBool(Utility.Constants.Animation.FALLING, false);
            //     _animator.SetBool(Utility.Constants.Animation.JUMP, false);
            // }

            _amount = new Vector3(direction * _movementAmount.x * (shiftPressed? 1.0f : 0.5f), 0 * _movementAmount.y, 0);
            _jump = _grounded && InputHandler.Instance.Space;

            UpdateAnimation(direction, shiftPressed? 1.0f : 0.0f);

            if(!_jumpProceeded)
            {
                _body.velocity = (Vector2.up * _movementAmount.y);
                _jumpProceeded = true;
            }
            if(_grounded)
                _body.velocity = new Vector2(_amount.x, _body.velocity.y);
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
                _animator.SetBool(Utility.Constants.Animation.JUMP, _jump);
                _jump = false;
                _jumpProceeded = false;
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
            BlockInput = true;
            _animator.SetBool(Utility.Constants.Animation.DEAD, true);
            _amount = Vector2.zero;
        }
    }
}