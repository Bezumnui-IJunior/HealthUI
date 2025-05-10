using Attributes;
using TMPro;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextBar : MonoBehaviour
    {
        [SerializeField, Restrict(typeof(IChangeableValue))]
        private Object _changeable;

        private TextMeshProUGUI _textMeshPro;

        private IChangeableValue Changeable => _changeable as IChangeableValue;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            Changeable.Decreased += SetValue;
            Changeable.Increased += SetValue;
            SetValue();
        }

        private void OnDisable()
        {
            Changeable.Decreased -= SetValue;
            Changeable.Increased -= SetValue;
        }

        private void SetValue()
        {
            _textMeshPro.text = $"{Changeable.Value}/{Changeable.MaxValue}";
        }
    }
}