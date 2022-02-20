using UnityEngine;


namespace BlueHood.Navigation
{
    public class IntermediatePoint : General.ScenePoint
    {
        [SerializeField] protected Color _color;

        public override Color Color
        {
            get => _color;
        }
    }
}
