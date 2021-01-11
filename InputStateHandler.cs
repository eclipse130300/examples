using System;
using Studio3.TwistToWin.Logic.Interfaces;
using Studio3.TwistToWin.Logic.Interfaces.enums;

namespace Studio3.TwistToWin.Logic.Input
{
    //it should just change the final input state +
    //connect clear input with changed one
    public class InputStateHandler : IInputStateHandler, IDisposable
    {
        private readonly IInputRaiser _finalInputRaiser;
        private InputState _inputState;

        private IInput _basicInput;

        public InputStateHandler(IInputRaiser finalInputRaiser)
        {
            _finalInputRaiser = finalInputRaiser;
        }

        public void Initialize(IInput processingInput)
        {
            _basicInput = processingInput;
            SetActive(true);
        }

        public void ChangeInputState(InputState state)
        {
            _inputState = state;
        }

        public void SetActive(bool isOn)
        {
            if (isOn)
            {
                _basicInput.RotateLeft += MainInputOnRotateLeft;
                _basicInput.RotateRight += MainInputOnRotateRight;
                _basicInput.SpeedUpStart += MainInputOnSpeedUpStart;
                _basicInput.SpeedUpStop += MainInputOnSpeedUpStop;
                _basicInput.FlipHorizontal += MainInputOnFlipHorizontal;
            }
            else
            {
                _basicInput.RotateLeft -= MainInputOnRotateLeft;
                _basicInput.RotateRight -= MainInputOnRotateRight;
                _basicInput.SpeedUpStart -= MainInputOnSpeedUpStart;
                _basicInput.SpeedUpStop -= MainInputOnSpeedUpStop;
                _basicInput.FlipHorizontal -= MainInputOnFlipHorizontal;
            }
        }

        private void MainInputOnRotateRight()
        {
            switch (_inputState)
            {
                case InputState.Reversed:
                    _finalInputRaiser.RaiseRotateLeft();
                    break;

                default:
                    _finalInputRaiser.RaiseRotateRight();
                    break;
            }
        }

        private void MainInputOnRotateLeft()
        {
            switch (_inputState)
            {
                case InputState.Reversed:
                    _finalInputRaiser.RaiseRotateRight();
                    break;

                default:
                    _finalInputRaiser.RaiseRotateLeft();
                    break;
            }
        }

        private void MainInputOnFlipHorizontal()
        {
            _finalInputRaiser.RaiseFlipHorizontal();
        }

        private void MainInputOnSpeedUpStop()
        {
            //todo: think about this mechanics
           // _finalInputRaiser.RaiseSpeedUpStop();
        }

        private void MainInputOnSpeedUpStart()
        {
            switch (_inputState)
            {
                case InputState.BoostDisabled:
                    break;

                default:
                    _finalInputRaiser.RaiseSpeedUpStart();
                    break;
            }
        }

        public void Dispose()
        {
            SetActive(false);
        }
    }
}