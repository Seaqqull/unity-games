using UnityEngine;


namespace BlueHood.Utility.Constants
{
    public static class Animation
    {
        public static readonly int WALK_SPEED = Animator.StringToHash("WalkSpeed");
        public static readonly int GROUNDED = Animator.StringToHash("IsGrounded");
        public static readonly int DIRECTION = Animator.StringToHash("Direction");
        public static readonly int ATTACK = Animator.StringToHash("IsAttacking");
        public static readonly int RUNNING = Animator.StringToHash("IsRunning");
        public static readonly int FALLING = Animator.StringToHash("IsFalling");
        public static readonly int DAMAGE = Animator.StringToHash("Damage");
        public static readonly int JUMP = Animator.StringToHash("IsJumping");
        public static readonly int DEAD = Animator.StringToHash("Die");
    }
}