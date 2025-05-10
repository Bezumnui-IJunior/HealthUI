using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    public class AnimatedBar : MonoBehaviour, ICoroutineExecutor
    {
        [SerializeField] private Health _health;
        [SerializeField] private float _delta;
        [SerializeField] private float _colorDelaySeconds;

        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _damageColor = Color.red;
        [SerializeField] private Color _healColor = Color.cyan;
        [SerializeField] private Image _fillTexture;

        private Slider _slider;
        private Coroutine _coroutine;
        private WaitForEndOfFrameUnit _delay;
        private ColorChanger _colorChanger;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _delay = new WaitForEndOfFrameUnit();
            _colorChanger = new ColorChanger(this, _fillTexture, _colorDelaySeconds);
        }

        private void OnEnable()
        {
            _health.Damaged += OnDamaged;
            _health.Healed += OnHealed;
        }

        private void OnDisable()
        {
            _health.Damaged -= OnDamaged;
            _health.Healed -= OnHealed;
        }

        private void OnDamaged()
        {
            _colorChanger.SetDamageColor(_damageColor);
            ChangeValue();
        }

        private void OnHealed()
        {
            _colorChanger.SetDamageColor(_healColor);
            ChangeValue();
        }

        private IEnumerator ChangingValue()
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            while (_slider.value != _health.Value)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, _health.Value, _delta * Time.deltaTime);

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