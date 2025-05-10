using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public readonly float MaxHealth = 100;
    private const float MinHealth = 0;
    public float Value { get; private set; } = 100;

    public event Action Damaged;
    public event Action Healed;

    public void Damage(float value)
    {
        if (value <= 0)
            return;

        Value = Mathf.Max(MinHealth, Value - value);
        Damaged?.Invoke();
    }

    public void Heal(float value)
    {
        if (value <= 0)
            return;

        Value = Mathf.Min(MaxHealth, Value + value);
        Healed?.Invoke();
    }
}