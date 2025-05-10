using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    public class SimpleBar : MonoBehaviour
    {
        [SerializeField] private Health _health;
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _health.Damaged += OnChanged;
            _health.Healed += OnChanged;
        }

        private void OnChanged()
        {
            _slider.value = _health.Value;
        }
    }
}