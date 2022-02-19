using UnityEngine;


namespace BlueHood.Animation
{
    public class AnimationDirection : Base.BaseBehaviour
    {
        [SerializeField] private bool _reverse;

        private Animator _controller;


        protected override void Awake()
        {
            base.Awake();

            _controller = GetComponent<Animator>();
        }

        private void Update()
        {
            int direction = _controller.GetInteger(Utility.Constants.Animation.DIRECTION);

            if(direction != 0)
                Transform.localScale = new Vector3(((_reverse) ? (-direction) : direction), 1, 1);
        }
    }
}
