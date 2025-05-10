using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HealButton : MonoBehaviour
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
            _health.Heal(_value);
        }
    }
}