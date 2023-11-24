using UnityEngine;
using UniRx;

namespace System
{
    public class SlotRotationManager : MonoBehaviour
    {
        private readonly Subject<int> _rotationSubject = new Subject<int>();

        public IObservable<int> BeginDragPiece => _rotationSubject;

        public void SendPieceRotation(int rotation)
        {
            _rotationSubject.OnNext(rotation);
        }
    }
}
