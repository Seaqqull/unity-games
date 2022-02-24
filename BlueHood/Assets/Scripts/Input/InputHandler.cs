using EngineInput = UnityEngine.Input;
using BlueHood.Base;
using UnityEngine;


namespace BlueHood.Input
{
    public class InputHandler : SingleBehaviour<InputHandler>
    {
        public float Horizontal
        {
            get; private set;
        }
        public float Vertical
        {
            get; private set;
        }
        public bool Space
        {
            get; private set;
        }
        public bool Shift
        {
            get; private set;
        }

        public bool E
        {
            get; private set;
        }


        private void Update()
        {
            // General
            Horizontal = EngineInput.GetAxisRaw("Horizontal");
            Vertical = EngineInput.GetAxisRaw("Vertical");

            // Special
            Shift = EngineInput.GetKey(KeyCode.LeftShift) || EngineInput.GetKey(KeyCode.RightShift);
            Space = EngineInput.GetKeyDown(KeyCode.Space);
            E = EngineInput.GetKey(KeyCode.E);
        }
    }
}
