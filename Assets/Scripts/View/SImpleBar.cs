using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Slider))]
    public class SimpleBar : Bar
    {
        protected override void OnDecreased()
        {
            Slider.value = Changeable.Value;
        }

        protected override void OnIncreased()
        {
            Slider.value = Changeable.Value;
        }
    }
}