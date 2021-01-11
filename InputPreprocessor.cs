using Studio3.TwistToWin.Logic.Interfaces;

namespace Studio3.TwistToWin.Logic.Input
{
    public class InputPreprocessor
    {
        private readonly IInput _activeInput;

        public InputPreprocessor()
        {
#if UNITY_EDITOR
            _activeInput = new KeyboardInput();
#else
            _activeInput = new MobileInput();
#endif
        }

        public IInput GetPlatformInput()
        {
            return _activeInput;
        }
    }
}