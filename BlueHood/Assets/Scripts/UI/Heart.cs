using UnityEngine;


namespace BlueHood.UI
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private GameObject _filledImage;

        
        public void FillHeart(bool fillState)
        {
            _filledImage.SetActive(fillState);
        }
    }
}
