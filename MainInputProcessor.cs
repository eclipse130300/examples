using JetBrains.Annotations;
using Studio3.TwistToWin.Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Studio3.TwistToWin.Logic.Input
{
    public class MainInputProcessor : MonoBehaviour
    {
        [Inject] [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        private readonly InputPreprocessor _inputPreprocessor;

        [Inject] [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        private readonly IInputStateHandler _inputStateHandler;

        private IInput _mainInput = new NullInput();

        private void Awake()
        {
            _mainInput = _inputPreprocessor.GetPlatformInput();
            _inputStateHandler.Initialize(_mainInput);
        }

        private void Update()
        {
            _mainInput.Tick();
        }
    }
}