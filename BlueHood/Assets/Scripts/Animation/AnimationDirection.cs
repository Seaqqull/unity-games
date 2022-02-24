using UnityEngine;


namespace BlueHood.Animation
{
    public class AnimationDirection : Base.BaseBehaviour
    {
        #region Constants
        private const float SIDE_ROTATION_AMOUNT = 180.0f;
        #endregion
        
        
        [SerializeField] private bool _reverse;

        private Animator _controller;


        protected override void Awake()
        {
            base.Awake();

            _controller = GetComponent<Animator>();
        }

        private void Update()
        {
            var direction = _controller.GetInteger(Utility.Constants.Animation.DIRECTION);
            var rotationAngle = (direction == 1 && _reverse) || (direction == -1)
                ? SIDE_ROTATION_AMOUNT
                : 0; 
            var rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

            if(direction != 0)
                Transform.rotation = rotation;
        }
    }
}
