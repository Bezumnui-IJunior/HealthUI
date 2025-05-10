using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Button))]
    public class DamageButton : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private float _value = 15;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _health.Damage(_value);
        }
    }
}