using Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField, Restrict(typeof(IChangeableValue))]
        private Object _changeable;

        protected Slider Slider { get; private set; }

        protected IChangeableValue Changeable => _changeable as IChangeableValue;

        protected virtual void Awake()
        {
            Slider = GetComponent<Slider>();
            Slider.minValue = Changeable.MinValue;
            Slider.maxValue = Changeable.MaxValue;
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

        protected abstract void OnDecreased();
        protected abstract void OnIncreased();
    }
}