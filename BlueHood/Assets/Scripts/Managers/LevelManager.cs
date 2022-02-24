using UnityEngine.SceneManagement;
using BlueHood.Base;


namespace BlueHood.Managers
{
    public class LevelManager : SingleBehaviour<LevelManager>
    {
        public Behaviour.Player Player { get; private set; }

        private int _coinsCount;
        

        private void Start()
        {
            Player = FindObjectOfType<Behaviour.Player>();
        }

        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void AddCoin(int amount = 1)
        {
            _coinsCount += amount;
            UIManager.Instance.UpdateCoinsCount(_coinsCount);
        }
    }
}
