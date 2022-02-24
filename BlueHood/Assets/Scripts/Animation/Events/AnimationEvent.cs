using UnityEngine.Events;
using UnityEngine;


namespace BlueHood.Animation.Events
{
    public class AnimationEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onEvent;
        
        
        public virtual void OnEvent()
        {
            _onEvent?.Invoke();
        }
    }
}