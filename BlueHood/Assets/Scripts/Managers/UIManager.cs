using BlueHood.Base;
using UnityEngine;
using TMPro;


namespace BlueHood.Managers
{
    public class UIManager : SingleBehaviour<UIManager>
    {
        [SerializeField] private TMP_Text _coinsText;


        public void UpdateCoinsCount(int amount)
        {
            _coinsText.text = amount.ToString();
        }
    }
}
