using System;
using UnityEngine;

public class Health : MonoBehaviour, IChangeableValue
{
    public float MaxValue => 100;
    public float MinValue => 0;

    public float Value { get; private set; } = 100;

    public event Action Decreased;
    public event Action Increased;

    public void Damage(float value)
    {
        if (value <= 0)
            return;

        Value = Mathf.Max(MinValue, Value - value);
        Decreased?.Invoke();
    }

    public void Heal(float value)
    {
        if (value <= 0)
            return;

        Value = Mathf.Min(MaxValue, Value + value);
        Increased?.Invoke();
    }
}