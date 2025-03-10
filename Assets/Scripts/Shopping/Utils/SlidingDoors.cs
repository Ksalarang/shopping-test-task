using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Shopping.Extensions;
using UnityEngine;

namespace Shopping.Utils
{
    public class SlidingDoors : MonoBehaviour
    {
        [SerializeField]
        private DoorState _state = DoorState.Closed;

        [SerializeField]
        private Transform _left;

        [SerializeField]
        private Transform _right;

        [SerializeField]
        private TriggerEventSender _doorTrigger;

        [SerializeField]
        private float _duration = 0.5f;

        private Vector3 _leftDoorClosedPosition;
        private Vector3 _rightDoorClosedPosition;
        private Vector3 _leftDoorOpenPosition;
        private Vector3 _rightDoorOpenPosition;

        private CancellationTokenSource _tokenSource;

        private void Awake()
        {
            _leftDoorClosedPosition = _leftDoorOpenPosition = _left.localPosition;
            _rightDoorClosedPosition = _rightDoorOpenPosition = _right.localPosition;

            var offset = 0.1f; // to avoid clipping
            _leftDoorOpenPosition.x = _leftDoorClosedPosition.x - _left.localScale.x - offset;
            _rightDoorOpenPosition.x = _rightDoorClosedPosition.x + _right.localScale.x + offset;

            _doorTrigger.OnTriggerEntered += OnTriggerEntered;
            _doorTrigger.OnTriggerExited += OnTriggerExited;
        }

        private void OnDestroy()
        {
            _doorTrigger.OnTriggerEntered -= OnTriggerEntered;
            _doorTrigger.OnTriggerExited -= OnTriggerExited;
            _tokenSource?.CancelAndDispose();
        }

        private void OnTriggerEntered(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OpenAsync(GetToken()).Forget();
            }
        }

        private void OnTriggerExited(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CloseAsync(GetToken()).Forget();
            }
        }

        private async UniTask OpenAsync(CancellationToken token)
        {
            if (_state == DoorState.Open)
            {
                return;
            }

            _state = DoorState.Opening;
            _left.DOLocalMoveX(_leftDoorOpenPosition.x, _duration).WithCancellation(token).Forget();
            await _right.DOLocalMoveX(_rightDoorOpenPosition.x, _duration).WithCancellation(token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            _state = DoorState.Open;
        }

        private async UniTask CloseAsync(CancellationToken token)
        {
            if (_state == DoorState.Closed)
            {
                return;
            }

            _state = DoorState.Closing;
            _left.DOLocalMoveX(_leftDoorClosedPosition.x, _duration).WithCancellation(token).Forget();
            await _right.DOLocalMoveX(_rightDoorClosedPosition.x, _duration).WithCancellation(token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            _state = DoorState.Closed;
        }

        private CancellationToken GetToken()
        {
            _tokenSource?.CancelAndDispose();

            _tokenSource = new CancellationTokenSource();
            return _tokenSource.Token;
        }

        private enum DoorState
        {
            Closed,
            Opening,
            Open,
            Closing,
        }
    }
}