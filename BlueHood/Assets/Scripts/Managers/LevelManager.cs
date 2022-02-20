using BlueHood.Base;


namespace BlueHood.Managers
{
    public class LevelManager : SingleBehaviour<LevelManager>
    {
        public Behaviour.Player Player { get; private set; }
        

        private void Start()
        {
            Player = FindObjectOfType<Behaviour.Player>();
        }  
    }
}
