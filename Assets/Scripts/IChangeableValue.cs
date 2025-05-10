using System;

public interface IChangeableValue
{
    float Value { get; }
    public float MaxValue { get; }
    public float MinValue { get; }

    event Action Decreased;
    event Action Increased;
}