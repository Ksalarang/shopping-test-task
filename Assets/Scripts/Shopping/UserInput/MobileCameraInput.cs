using JetBrains.Annotations;
using Shopping.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Shopping.UserInput
{
    [UsedImplicitly]
    public class MobileCameraInput : ICameraInput, ITickable
    {
        public Vector2 Delta => _delta;

        [Inject]
        private GraphicRaycaster _graphicRaycaster;

        [Inject]
        private EventSystem _eventSystem;

        private Vector2 _delta;
        private bool _overUi;
        private int _currentIndex = -1;

        public void Tick()
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.touches[i];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (_currentIndex == -1)
                        {
                            var results = GraphicUtils.Raycast(_graphicRaycaster, _eventSystem, touch.position);
                            _overUi = results.Count > 0;

                            if (_overUi == false)
                            {
                                _currentIndex = i;
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (i == _currentIndex)
                        {
                            _delta = touch.deltaPosition;
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (i == _currentIndex)
                        {
                            _delta = Vector2.zero;
                            _overUi = false;
                            _currentIndex = -1;
                        }
                        break;
                }
            }
        }
    }
}