using System.Collections;
using Attributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    public class AnimatedBar : MonoBehaviour, ICoroutineExecutor
    {
        [SerializeField, Restrict(typeof(IChangeableValue))]
        private Object _changeable;

        [SerializeField] private float _delta;
        [SerializeField] private float _colorDelaySeconds;

        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _decreaseColor = Color.red;
        [SerializeField] private Color _increaseColor = Color.cyan;
        [SerializeField] private Image _fillTexture;

        private Slider _slider;
        private Coroutine _coroutine;
        private WaitForEndOfFrameUnit _delay;
        private ColorChanger _colorChanger;

        private IChangeableValue Changeable => _changeable as IChangeableValue;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _delay = new WaitForEndOfFrameUnit();
            _colorChanger = new ColorChanger(this, _fillTexture, _colorDelaySeconds);
            _slider.minValue = Changeable.MinValue;
            _slider.maxValue = Changeable.MaxValue;
        }

        private void OnEnable()
        {
            Changeable.Decreased += OnDecreased;
            Changeable.Increased += OnIncreased;
        }

        private void OnDisable()
        {
            Changeable.Decreased -= OnDecreased;
            Changeable.Increased -= OnIncreased;
        }

        private void OnDecreased()
        {
            _colorChanger.SetDamageColor(_decreaseColor);
            ChangeValue();
        }

        private void OnIncreased()
        {
            _colorChanger.SetDamageColor(_increaseColor);
            ChangeValue();
        }

        private IEnumerator ChangingValue()
        {
            while (Mathf.Approximately(_slider.value, Changeable.Value) == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, Changeable.Value, _delta * Time.deltaTime);

                yield return _delay;
            }

            _colorChanger.SetDefaultDelayed(_defaultColor);
        }

        private void ChangeValue()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangingValue());
        }
    }
}