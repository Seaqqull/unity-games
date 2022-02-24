using UnityEngine;


namespace BlueHood.Animation.Events
{
    public class DestroyObjectAnimationEvent : AnimationEvent
    {
        [SerializeField] private GameObject _objectToBeDestroyed;
        
        
        public override void OnEvent()
        {
            Destroy(_objectToBeDestroyed);
        }
    }
}
