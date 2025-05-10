using TMPro;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextBar : MonoBehaviour
    {
        [SerializeField] private Health _health;
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _health.Damaged += SetValue;
            _health.Healed += SetValue;
            SetValue();
        }

        private void OnDisable()
        {
            _health.Damaged -= SetValue;
            _health.Healed -= SetValue;
        }

        private void SetValue()
        {
            _textMeshPro.text = $"{_health.Value}/{_health.MaxHealth}";
        }
    }
}