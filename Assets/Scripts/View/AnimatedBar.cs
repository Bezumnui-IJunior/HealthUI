using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    public class AnimatedBar : Bar, ICoroutineExecutor
    {
        [SerializeField] private float _delta;
        [SerializeField] private float _colorDelaySeconds;

        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _decreaseColor = Color.red;
        [SerializeField] private Color _increaseColor = Color.cyan;
        [SerializeField] private Image _fillTexture;

        private Coroutine _coroutine;
        private WaitForEndOfFrameUnit _delay;
        private ColorChanger _colorChanger;

        protected override void Awake()
        {
            base.Awake();
            _delay = new WaitForEndOfFrameUnit();
            _colorChanger = new ColorChanger(this, _fillTexture, _colorDelaySeconds);
        }

        protected override void OnDecreased()
        {
            ChangeValueWithColor(_decreaseColor);
        }

        protected override void OnIncreased()
        {
            ChangeValueWithColor(_increaseColor);
        }

        private void ChangeValueWithColor(Color color)
        {
            _colorChanger.SetDamageColor(color);
            ChangeValue();
        }

        private IEnumerator ChangingValue()
        {
            while (Mathf.Approximately(Slider.value, Changeable.Value) == false)
            {
                Slider.value = Mathf.MoveTowards(Slider.value, Changeable.Value, _delta * Time.deltaTime);

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