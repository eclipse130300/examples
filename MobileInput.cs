using System;
using Studio3.TwistToWin.Logic.Interfaces;
using UnityEngine;
using static UnityEngine.Input;

namespace Studio3.TwistToWin.Logic.Input
{
    public class MobileInput : IInput
    {
        private const int SwipePixelsThresholdDistance = 120; //pixels
        private const float SwipeAngle = 0.3f;

        public  event Action RotateRight;
        public  event Action RotateLeft;
        public  event Action FlipVertical;
        public  event Action FlipHorizontal;
        public  event Action SpeedUpStart;
        public  event Action SpeedUpStop;
        public  event Action AddNewCell;
        public  event Action ChangePlatformColor;
        
        private Vector2 _startingTouchPos;
        private readonly int _screenAdjustedThreshold;
        private bool _canSwipe;
        
        public MobileInput()
        {
            var screenDiffRatio = (float) Screen.width / 1080;
            _screenAdjustedThreshold = Mathf.RoundToInt(SwipePixelsThresholdDistance * screenDiffRatio);
        }

        public void Tick()
        {
            if (touchCount >= 1)
            {
                if(!_canSwipe) return;
                
                Touch touch = GetTouch(0);
                Vector2 currentTouchPos = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    _startingTouchPos = currentTouchPos;
                }

                else if (touch.phase == TouchPhase.Moved)
                {
                    float swipeDelta = (currentTouchPos - _startingTouchPos).magnitude;

                    if (swipeDelta > _screenAdjustedThreshold)
                    {
                        Vector2 swipeDir = (currentTouchPos - _startingTouchPos).normalized;
                        float swipeAngleReversed = 1 - SwipeAngle;

                        //we compare directions and handle swipes
                        if (Vector2.Dot(swipeDir, Vector2.up) > swipeAngleReversed)
                        {
                            //we swiped Up
                            OnSwipeUp();
                        }
                        else if (Vector2.Dot(swipeDir, Vector2.left) > swipeAngleReversed)
                        {
                            OnSwipeLeft();
                        }
                        else if (Vector2.Dot(swipeDir, Vector2.right) > swipeAngleReversed)
                        {
                            OnSwipeRight();
                        }
                        else if (Vector2.Dot(swipeDir, Vector2.down) > swipeAngleReversed)
                        {
                            OnSwipeDown();
                        }

                        _canSwipe = false;
                    }
                }
            }
            else
            {
                _canSwipe = true;
            }
        }
        public void OnSwipeLeft()
        {
            RotateLeft?.Invoke();
            Debug.Log("SwipeLeft");
        }

        public void OnSwipeRight()
        {
            RotateRight?.Invoke();
            Debug.Log("SwipeRight");
        }

        public void OnSwipeUp()
        {
            SpeedUpStart?.Invoke();
            Debug.Log("SwipeUP");
        }

        public void OnSwipeDown()
        {
            SpeedUpStop?.Invoke();
            Debug.Log("SwipeDown");
        }

        public void OnTap()
        {
            FlipHorizontal?.Invoke();
        }
    }
}