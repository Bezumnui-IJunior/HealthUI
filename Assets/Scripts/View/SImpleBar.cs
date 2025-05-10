using Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    public class SimpleBar : MonoBehaviour
    {
        [SerializeField, Restrict(typeof(IChangeableValue))]
        private Object _changeable;

        private Slider _slider;

        private IChangeableValue Changeable => _changeable as IChangeableValue;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.minValue = Changeable.MinValue;
            _slider.maxValue = Changeable.MaxValue;
        }

        private void OnEnable()
        {
            Changeable.Decreased += OnChanged;
            Changeable.Increased += OnChanged;
        }

        private void OnDisable()
        {
            Changeable.Decreased -= OnChanged;
            Changeable.Increased -= OnChanged;
        }

        private void OnChanged()
        {
            _slider.value = Changeable.Value;
        }
    }
}