using UnityEngine;


namespace BlueHood.UI
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private Heart[] _hearts;
        
        
        public void OnHealthChange(float percent)
        {
            for (int i = _hearts.Length - 1; i >= 0; i--)
            {
                var heartFilled = (((i + 1.0f) / _hearts.Length) <= percent);
                _hearts[i].FillHeart(heartFilled);
            }
        }
    }
}
