using Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace System
{
    public class ButtonController : MonoBehaviour
    {
        private readonly Subject<bool> _pushed = new Subject<bool>();
        public IObservable<bool> Pushed => _pushed;
        private Button _button;
        [SerializeField,Tooltip("�炷SE�̉��̎�ނ�ݒ�")] private SeType se;

        private void Start()
        {
            _button = gameObject.GetComponent<Button>();

            _button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _pushed.OnNext(true);
                    SeManager.Instance.ShotSe(se);
                })
                .AddTo(this);
        }
    }
}
