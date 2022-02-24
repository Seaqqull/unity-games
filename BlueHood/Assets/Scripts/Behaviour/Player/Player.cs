using UnityEngine;


namespace BlueHood.Behaviour
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class Player : Entity
    {
        [SerializeField][Range(0.0f, 1.0f)] private float _forceInputBlockTime = 0.1f;
        
        private PlayerInputHandler _input;


        protected override void Awake()
        {
            base.Awake();

            _input = GetComponent<PlayerInputHandler>();
        }


        public override void ApplyForce(Vector2 direction, float power, ForceMode2D force = ForceMode2D.Force)
        {
            base.ApplyForce(direction, power, force);

            _input.MuteInput(_forceInputBlockTime);
        }
    }
}